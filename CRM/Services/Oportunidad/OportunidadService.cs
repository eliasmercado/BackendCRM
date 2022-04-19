using CRM.DTOs.Oportunidad;
using CRM.Helpers;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Oportunidad
{
    public class OportunidadService
    {
        private CrmDbContext _context;

        public OportunidadService(CrmDbContext context)
        {
            _context = context;
        }

        public List<OportunidadDTO> ObtenerListaPoportunidades()
        {
            List<OportunidadDTO> listaOportunidades = (from oportunidad in _context.Oportunidads
                                                 select new OportunidadDTO()
                                                 {
                                                    IdOportunidad = oportunidad.IdOportunidad,
                                                    IdSucursal = oportunidad.IdSucursal,
                                                    FechaCreacion = oportunidad.FechaCreacion,
                                                    FechaCierre = oportunidad.FechaCierre,
                                                    IdContacto = oportunidad.IdContacto,
                                                    IdEmpresa = oportunidad.IdEmpresa,
                                                    IdEtapa = oportunidad.IdEtapa,
                                                    IdFuente = oportunidad.IdFuente,
                                                    IdPrioridad = oportunidad.IdPrioridad,
                                                    IdPropietario = oportunidad.IdPropietario,
                                                    Nombre = oportunidad.Nombre,
                                                    Valor = oportunidad.Valor,
                                                    Observacion = oportunidad.Observacion

                                                 }).ToList();

            return listaOportunidades;
        }

        public OportunidadDTO ObtenerOportunidadById(int id)
        {
            OportunidadDTO oportunidadDTO = (from oportunidad in _context.Oportunidads
                                           where oportunidad.IdOportunidad == id
                                           select new OportunidadDTO()
                                           {
                                               IdOportunidad = oportunidad.IdOportunidad,
                                               IdSucursal = oportunidad.IdSucursal,
                                               FechaCreacion = oportunidad.FechaCreacion,
                                               FechaCierre = oportunidad.FechaCierre,
                                               IdContacto = oportunidad.IdContacto,
                                               IdEmpresa = oportunidad.IdEmpresa,
                                               IdEtapa = oportunidad.IdEtapa,
                                               IdFuente = oportunidad.IdFuente,
                                               IdPrioridad = oportunidad.IdPrioridad,
                                               IdPropietario = oportunidad.IdPropietario,
                                               Nombre = oportunidad.Nombre,
                                               Valor = oportunidad.Valor,
                                               Observacion = oportunidad.Observacion

                                           }).FirstOrDefault();

            return oportunidadDTO;
        }

        public string ModificarOportunidad(int id, OportunidadDTO oportunidadModificada)
        {
            if (id != oportunidadModificada.IdSucursal)
            {
                throw new ApiException("Identificador de Oportunidad no válido");
            }

            Models.Oportunidad oportunidad = _context.Oportunidads.Find(id);

            if (oportunidad == null)
                throw new ApiException("La oportunidad no existe.");

            oportunidad.IdSucursal = oportunidadModificada.IdSucursal;
            oportunidad.IdEtapa = oportunidadModificada.IdEtapa;
            oportunidad.IdPrioridad = oportunidadModificada.IdPrioridad;
            oportunidad.IdPropietario = oportunidadModificada.IdPropietario;
            oportunidad.Nombre = oportunidadModificada.Nombre;
            oportunidad.Valor = oportunidadModificada.Valor;
            oportunidad.Observacion = oportunidadModificada.Observacion;

            _context.SaveChanges();

            return "La Oportunidad se modificó correctamente.";
        }

        public string CrearOportunidad(OportunidadDTO oportunidadlNueva)
        {

            Models.Oportunidad oportunidad = new()
            {
                IdSucursal = oportunidadlNueva.IdSucursal,
                FechaCreacion = oportunidadlNueva.FechaCreacion,
                FechaCierre = oportunidadlNueva.FechaCierre,
                IdContacto = oportunidadlNueva.IdContacto,
                IdEmpresa = oportunidadlNueva.IdEmpresa,
                IdEtapa = oportunidadlNueva.IdEtapa,
                IdFuente = oportunidadlNueva.IdFuente,
                IdPrioridad = oportunidadlNueva.IdPrioridad,
                IdPropietario = oportunidadlNueva.IdPropietario,
                Nombre = oportunidadlNueva.Nombre,
                Valor = oportunidadlNueva.Valor,
                Observacion = oportunidadlNueva.Observacion

            };
            _context.Oportunidads.Add(oportunidad);
            _context.SaveChanges();

            return "La sucuoportunidadrsal se agregó correctamente.";
        }
    }
}
