using CRM.DTOs.Producto;
using CRM.Helpers;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.CategoriaService
{
    public class CategoriaService
    {
        private CrmDbContext _context;

        public CategoriaService(CrmDbContext context)
        {
            _context = context;
        }

        public List<CategoriaDTO> ObtenerListaCategorias()
        {
            List<CategoriaDTO> listaCategorias = (from categoria in _context.Categoria
                                                  where categoria.IdCategoriaPadre == null
                                                  select new CategoriaDTO()
                                                  {
                                                      IdCategoria = categoria.IdCategoria,
                                                      Nombre = categoria.Nombre, 
                                                      Descripcion = categoria.Descripcion,
                                                      Estado = categoria.Estado
                                                  }).ToList();
            return listaCategorias;
        }

        public List<CategoriaDTO> ObtenerListaSubCategorias()
        {
            List<CategoriaDTO> listaCategorias = (from categoria in _context.Categoria
                                                  where categoria.IdCategoriaPadre != null
                                                  select new CategoriaDTO()
                                                  {
                                                      IdCategoria = categoria.IdCategoria,
                                                      Nombre = categoria.Nombre,
                                                      Descripcion = categoria.Descripcion,
                                                      IdCategoriaPadre = categoria.IdCategoriaPadre,
                                                      Estado = categoria.Estado
                                                  }).ToList();

            return listaCategorias;
        }

        public CategoriaDTO ObtenerCategoriaById(int id)
        {
            CategoriaDTO categoriaDTO = (from categoria in _context.Categoria
                                 where categoria.IdCategoria == id
                                 select new CategoriaDTO()
                                 {
                                     IdCategoria = categoria.IdCategoria,
                                     Nombre = categoria.Nombre,
                                     Descripcion = categoria.Descripcion,
                                     IdCategoriaPadre = categoria.IdCategoriaPadre,
                                     Estado = categoria.Estado
                                 }).FirstOrDefault();

            return categoriaDTO;
        }

        public List<CategoriaDTO> ObtenerSubCategoriasByIdPadre(int idCategoriaPadre)
        {
            List<CategoriaDTO> categoriasDTO = (from categoria in _context.Categoria
                                         where categoria.IdCategoriaPadre == idCategoriaPadre
                                         select new CategoriaDTO()
                                         {
                                             IdCategoria = categoria.IdCategoria,
                                             Nombre = categoria.Nombre,
                                             Descripcion = categoria.Descripcion,
                                             IdCategoriaPadre = categoria.IdCategoriaPadre,
                                             Estado = categoria.Estado
                                         }).ToList();

            return categoriasDTO;
        }

        public string ModificarCategoria(int id, CategoriaDTO categoriaNueva)
        {
            if (id != categoriaNueva.IdCategoria)
            {
                throw new ApiException("Identificador de Categoria no válido");
            }

            Categoria categoria = _context.Categoria.Find(id);

            if (categoria == null)
                throw new ApiException("La categoria no existe");

            if (categoria.IdCategoriaPadre == null && categoria.Estado && !categoriaNueva.Estado && TieneCategoriasHijas(categoria.IdCategoria))
                throw new ApiException("Existen subcategorias activas para la categoría. No se puede deshabilitar");

            categoria.Nombre = categoriaNueva.Nombre;
            categoria.Descripcion = string.IsNullOrEmpty(categoriaNueva.Descripcion) ? null : categoriaNueva.Descripcion;
            categoria.IdCategoriaPadre = categoriaNueva.IdCategoriaPadre;
            categoria.Estado = categoriaNueva.Estado;

            _context.SaveChanges();

            return "La categoria modificó correctamente";
        }

        private bool TieneCategoriasHijas(int? idCategoriaPadre)
        {
            int cantidadCategorias = _context.Categoria.Where(x => x.IdCategoriaPadre == idCategoriaPadre && x.Estado).Count();

            return cantidadCategorias > 0;
        }

        public string CrearCategoria(CategoriaDTO categoriaNueva)
        {
            Categoria categoria = new()
            {
                Nombre = categoriaNueva.Nombre,
                Descripcion = string.IsNullOrEmpty(categoriaNueva.Descripcion) ? null : categoriaNueva.Descripcion,
                IdCategoriaPadre = categoriaNueva.IdCategoriaPadre
            };
            _context.Categoria.Add(categoria);
            _context.SaveChanges();

            return "La categoria se agregó correctamente";
        }

        public string EliminarCategoria(int id)
        {
            Categoria categoria = _context.Categoria.Where(x => x.Estado && x.IdCategoria == id).FirstOrDefault();

            if (categoria == null)
                throw new ApiException("La categoria no existe");

            categoria.Estado = false;

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return "La categoria se eliminó correctamente";
        }
    }
}
