using CRM.DTOs.Marca;
using CRM.Helpers;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;



namespace CRM.Services.MarcaService
{
    public class MarcaService

    {
        private CrmDbContext _context;

        public MarcaService(CrmDbContext context)
        {
            _context = context;
        }

        public List<MarcaDTO> ObtenerListaMarcas()
        {
            List<MarcaDTO> listaMarcas = (from marca in _context.Marcas
                                          where marca.Estado
                                          select new MarcaDTO()
                                               {
                                                   IdMarca = marca.IdMarca,
                                                   Descripcion = marca.Descripcion,
                                               }).ToList();

            return listaMarcas;
        }

        public MarcaDTO ObtenerMarcaById(int id)
        {
            MarcaDTO marcaDTO = (from marca in _context.Marcas 
                                    where marca.Estado && marca.IdMarca == id
                                    select new MarcaDTO()
                                   {
                                       IdMarca = marca.IdMarca,
                                       Descripcion = marca.Descripcion,

                                   }).FirstOrDefault();

            return marcaDTO;
        }

        public string ModificarMarca(int id, MarcaDTO marcaModificada)
        {
            if (id != marcaModificada.IdMarca)
            {
                throw new ApiException("Identificador de Marca no válido");
            }

            Marca marca = _context.Marcas.Find(id);

            if (marca == null)
                throw new ApiException("La marca no existe.");

            marca.IdMarca = marcaModificada.IdMarca;
            marca.Descripcion = marcaModificada.Descripcion;

            _context.SaveChanges();

            return "La marca modificó correctamente.";
        }

        public string CrearMarca(MarcaDTO marcaNueva)
        {
            Marca marca = new()
            {
                IdMarca = marcaNueva.IdMarca,
                Descripcion = marcaNueva.Descripcion
            };
            _context.Marcas.Add(marca);
            _context.SaveChanges();

            return "La marca se agregó correctamente.";
        }

        public string EliminarMarca(int id)
        {
            Marca marca = _context.Marcas.Where(x => x.Estado && x.IdMarca == id).FirstOrDefault();

            if (marca == null)
                throw new ApiException("La marca no existe.");

            marca.Estado = false;

            _context.Entry(marca).State = EntityState.Modified;
            _context.SaveChanges();

            return "La marca se eliminó correctamente.";
        }
    } 
}
