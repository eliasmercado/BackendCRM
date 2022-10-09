using CRM.DTOs.Sucursal;
using CRM.Helpers;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.SucursalService
{
    public class SucursalService

    {
        private CrmDbContext _context;

        public SucursalService(CrmDbContext context)
        {
            _context = context;
        }

        public List<SucursalDTO> ObtenerSucursales()
        {
            List<SucursalDTO> listaSucursales = (from sucursal in _context.Sucursals
                                          select new SucursalDTO()
                                          {
                                              IdSucursal = sucursal.IdSucursal,
                                              Descripcion = sucursal.Descripcion,
                                              Direccion = sucursal.Direccion,
                                              IdCiudad = sucursal.IdCiudad
                                          }).ToList();

            return listaSucursales;
        }

        public SucursalDTO ObtenerSucursal(int id)
        {
            SucursalDTO sucursalDTO = (from sucursal in _context.Sucursals
                                 where sucursal.IdSucursal == id
                                 select new SucursalDTO()
                                 {
                                     IdSucursal = sucursal.IdSucursal,
                                     Descripcion = sucursal.Descripcion,
                                     Direccion = sucursal.Direccion,
                                     IdCiudad = sucursal.IdCiudad
                                 }).FirstOrDefault();

            return sucursalDTO;
        }

        public string ModificarSucursal(int id, SucursalDTO sucursalModificada)
        {
            if (id != sucursalModificada.IdSucursal)
            {
                throw new ApiException("Identificador de Sucursal no válido");
            }

            Sucursal sucursal = _context.Sucursals.Find(id);

            if (sucursal == null)
                throw new ApiException("La sucursal no existe.");

            sucursal.Descripcion = string.IsNullOrEmpty(sucursalModificada.Descripcion) ? null : sucursalModificada.Descripcion;
            sucursal.Direccion = string.IsNullOrEmpty(sucursalModificada.Direccion) ? null : sucursalModificada.Direccion;
           
            _context.SaveChanges();

            return "La sucursal se modificó correctamente.";
        }

        public string CrearSucursal(SucursalDTO sucursalNueva)
        {
            Sucursal sucursal = new()
            {
                Direccion = string.IsNullOrEmpty(sucursalNueva.Direccion) ? null : sucursalNueva.Direccion,
                IdCiudad = sucursalNueva.IdCiudad,
                Descripcion = string.IsNullOrEmpty(sucursalNueva.Descripcion) ? null : sucursalNueva.Descripcion,
            };
            _context.Sucursals.Add(sucursal);
            _context.SaveChanges();

            return "La sucursal se agregó correctamente.";
        }

        public string EliminarSucursal(int id)
        {
            Sucursal sucursal = _context.Sucursals.Where(x => x.IdSucursal == id).FirstOrDefault();

            if (sucursal == null)
                throw new ApiException("La sucursal no existe.");


            _context.Entry(sucursal).State = EntityState.Deleted;
            _context.SaveChanges();

            return "La sucursal se eliminó correctamente.";
        }
    }

}
