using CRM.DTOs.Oportunidad;
using CRM.Helpers;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.SucursalService
{
    public class SucursalService
    {
        private CrmDbContext _context;

        public SucursalService(CrmDbContext context)
        {
            _context = context;
        }

        public List<SucursalDTO> ObtenerListaSucursales()
        {
            List<SucursalDTO> listaSucursales = (from sucursal in _context.Sucursals
                                          select new SucursalDTO()
                                          {
                                              IdSucursal = sucursal.IdSucursal,
                                              IdCiudad = sucursal.IdCiudad,
                                              Descripcion = sucursal.Descripcion,
                                              Direccion = sucursal.Direccion

                                          }).ToList();

            return listaSucursales;
        }

        public SucursalDTO ObtenerSucursalById(int id)
        {
            SucursalDTO sucursalDTO = (from sucursal in _context.Sucursals
                                 where sucursal.IdSucursal == id
                                 select new SucursalDTO()
                                 {
                                     IdSucursal = sucursal.IdSucursal,
                                     IdCiudad = sucursal.IdCiudad,
                                     Descripcion = sucursal.Descripcion,
                                     Direccion = sucursal.Direccion

                                 }).FirstOrDefault();

            return sucursalDTO;
        }

        public string ModificarSucursal(int id, SucursalDTO sucursalModificada)
        {
            if (id != sucursalModificada.IdSucursal)
            {
                throw new ApiException("Identificador de Sucursal no válido");
            }

            Models.Sucursal sucursal = _context.Sucursals.Find(id);

            if (sucursal == null)
                throw new ApiException("La sucursal no existe.");

            sucursal.Direccion = sucursalModificada.Direccion;
            sucursal.Descripcion = string.IsNullOrEmpty(sucursalModificada.Descripcion) ? null : sucursalModificada.Descripcion ;

            _context.SaveChanges();

            return "La sucursal se modificó correctamente.";
        }

        public string CrearSucursal(SucursalDTO sucursalNueva)
        {

            Models.Sucursal sucursal = new()
            {
                IdCiudad = sucursalNueva.IdCiudad,
                Descripcion = sucursalNueva.Descripcion,
                Direccion = sucursalNueva.Direccion,
                
            };
            _context.Sucursals.Add(sucursal);
            _context.SaveChanges();

            return "La sucursal se agregó correctamente.";
        }
    }
}
