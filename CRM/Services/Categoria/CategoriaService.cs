using CRM.DTOs.CategoriaDto;
using CRM.Helpers;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Categoria
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
                                          where categoria.Estado
                                          select new CategoriaDTO()
                                          {
                                              IdCategoria = categoria.IdCategoria,
                                              Descripcion = categoria.Descripcion,
                                              IdCategoriaPadre = categoria.IdCategoriaPadre
                                          }).ToList();

            return listaCategorias;
        }

        public List<CategoriaDTO> ObtenerListaCategoriasSinPadre()
        {
            List<CategoriaDTO> listaCategorias = (from categoria in _context.Categoria
                                                  where categoria.Estado && categoria.IdCategoriaPadre == null
                                                  select new CategoriaDTO()
                                                  {
                                                      IdCategoria = categoria.IdCategoria,
                                                      Descripcion = categoria.Descripcion,
                                                      
                                                  }).ToList();

            return listaCategorias;
        }

        public CategoriaDTO ObtenerCategoriaById(int id)
        {
            CategoriaDTO categoriaDTO = (from categoria in _context.Categoria
                                 where categoria.Estado && categoria.IdCategoria == id
                                 select new CategoriaDTO()
                                 {
                                     IdCategoria = categoria.IdCategoria,
                                     Descripcion = categoria.Descripcion,
                                     IdCategoriaPadre = categoria.IdCategoriaPadre

                                 }).FirstOrDefault();

            return categoriaDTO;
        }

        public string ModificarCategoria(int id, CategoriaDTO categoriaNueva)
        {
            if (id != categoriaNueva.IdCategoria)
            {
                throw new ApiException("Identificador de Categoria no válido");
            }

            Models.Categoria categoria = _context.Categoria.Find(id);

            if (categoria == null)
                throw new ApiException("La categoria no existe.");

            categoria.IdCategoria = categoriaNueva.IdCategoria;
            categoria.Descripcion = categoriaNueva.Descripcion;
            categoria.IdCategoriaPadre = categoriaNueva.IdCategoriaPadre;

            _context.SaveChanges();

            return "La categoria modificó correctamente.";
        }

        public string CrearCategoria(CategoriaDTO categoriaNueva)
        {
            Models.Categoria categoria = new()
            {
                IdCategoria = categoriaNueva.IdCategoria,
                Descripcion = categoriaNueva.Descripcion,
                IdCategoriaPadre = categoriaNueva.IdCategoriaPadre
            };
            _context.Categoria.Add(categoria);
            _context.SaveChanges();

            return "La categoria se agregó correctamente.";
        }

        public string EliminarCategoria(int id)
        {
            Models.Categoria categoria = _context.Categoria.Where(x => x.Estado && x.IdCategoria == id).FirstOrDefault();

            if (categoria == null)
                throw new ApiException("La categoria no existe.");

            categoria.Estado = false;

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return "La categoria se eliminó correctamente.";
        }
    }
}
