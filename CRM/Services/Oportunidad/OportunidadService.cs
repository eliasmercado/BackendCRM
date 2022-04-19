using CRM.DTOs.Oportunidad;
using CRM.Helpers;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.OportunidadService
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
            List<Oportunidad> listaOportunidades = (from oportunidad in _context.Oportunidads select oportunidad).ToList();
            List<OportunidadDTO> listaOportunidadesDto = new();
            OportunidadDTO oportunidadDto;
            InfoContacto contacto;

            foreach (var oportunidad in listaOportunidades)
            {
                oportunidadDto = new();
                contacto = new();
                oportunidadDto.IdOportunidad = oportunidad.IdOportunidad;
                oportunidadDto.Nombre = oportunidad.Nombre;
                oportunidadDto.FechaCierre = oportunidad.FechaCierre;
                oportunidadDto.Etapa = (from etapa in _context.Etapas where etapa.IdEtapa == oportunidad.IdEtapa select etapa.Descripcion).FirstOrDefault();
                oportunidadDto.IdEtapa = oportunidad.IdEtapa;
                oportunidadDto.Prioridad = (from prioridad in _context.Prioridads where prioridad.IdPrioridad == oportunidad.IdPrioridad select prioridad.Descripcion).FirstOrDefault();
                oportunidadDto.IdPrioridad = oportunidad.IdPrioridad;
                oportunidadDto.Valor = oportunidad.Valor;

                contacto = ObtenerContacto(oportunidad);
                oportunidadDto.TipoCliente = ObtenerTipoCliente(contacto);

                if (oportunidadDto.TipoCliente == Defs.CLIENTE_LEAD)
                {
                    if(contacto.TipoContacto == Defs.CLIENTE_PF)
                    {
                        oportunidadDto.LeadContacto = (ContactoAsociadoDTO)contacto.DatosContacto;
                        oportunidadDto.ContactoAsociado = oportunidadDto.LeadContacto.Nombres + " " + oportunidadDto.LeadContacto.Apellidos;
                    }
                    else
                    {
                        oportunidadDto.LeadEmpresa = (EmpresaAsociadaDTO)contacto.DatosContacto;
                        oportunidadDto.ContactoAsociado = oportunidadDto.LeadContacto.Nombres;
                    }
                }
                else
                {
                    if (contacto.TipoContacto == Defs.CLIENTE_PF)
                        oportunidadDto.IdContactoAsociado = ((ContactoAsociadoDTO)contacto.DatosContacto).IdContacto;
                    else
                        oportunidadDto.IdContactoAsociado = ((EmpresaAsociadaDTO)contacto.DatosContacto).IdEmpresa;
                }

                oportunidadDto.Detalles = (from detalle in _context.DetalleOportunidads where detalle.IdOportunidad == oportunidad.IdOportunidad
                                           select new DetalleOportunidadDTO() { 
                                                IdDetalleOportunidad = detalle.IdDetalleOportunidad,
                                                IdProducto = detalle.IdProducto,
                                                Cantidad = detalle.Cantidad
                                           }).ToList();

                oportunidadDto.Fuente = (from fuente in _context.Fuentes where fuente.IdFuente == oportunidad.IdFuente select fuente.Descripcion).FirstOrDefault();
                oportunidadDto.IdFuente= oportunidad.IdFuente;
                oportunidadDto.Observacion= oportunidad.Observacion;
                oportunidadDto.IdSucursal = oportunidad.IdSucursal;
                oportunidadDto.IdPropietario = oportunidad.IdPropietario;

                listaOportunidadesDto.Add(oportunidadDto);
            }

            return listaOportunidadesDto;
        }

        private InfoContacto ObtenerContacto(Oportunidad oportunidad)
        {
            InfoContacto respuesta = new();

            //Si es un cliente pf
            if (oportunidad.IdEmpresa == null)
            {
                ContactoAsociadoDTO contacto = (from contact in _context.Contactos
                                        where contact.Estado && !contact.EsLead && contact.IdContacto == oportunidad.IdContacto
                                                select new ContactoAsociadoDTO()
                                        {
                                            IdContacto = contact.IdContacto,
                                            Nombres = contact.Nombres,
                                            Apellidos = contact.Apellidos,
                                            Celular = contact.Celular,
                                            Email = contact.Email,
                                            FechaNacimiento = contact.FechaNacimiento,
                                            IdTipoDocumento = contact.IdTipoDocumento,
                                            Documento = contact.Documento,
                                            IdCiudad = contact.IdCiudad,
                                            IdDepartamento = _context.Ciudads.Where(x => x.IdCiudad == contact.IdCiudad).FirstOrDefault().IdDepartamento,
                                            Direccion = contact.Direccion,
                                            IdEstadoCivil = contact.IdEstadoCivil,
                                            IdActividadEconomica = contact.IdActividadEconomica,
                                            NombreEmpresa = contact.NombreEmpresa,
                                            DireccionLaboral = contact.DireccionLaboral,
                                            TelefonoLaboral = contact.TelefonoLaboral,
                                            CorreoLaboral = contact.CorreoLaboral,
                                            IdPropietario = contact.IdPropietario,
                                            EsLead = contact.EsLead
                                        }).FirstOrDefault();

                respuesta.TipoContacto = Defs.CLIENTE_PF;
            }
            //si es cliente pj
            else
            {
                EmpresaAsociadaDTO contacto = (from empresa in _context.Empresas
                                       where empresa.Estado && !empresa.EsLead && empresa.IdEmpresa == oportunidad.IdEmpresa
                                       select new EmpresaAsociadaDTO()
                                       {
                                           IdEmpresa = empresa.IdEmpresa,
                                           Nombre = empresa.Nombre,
                                           Celular = empresa.Celular,
                                           Telefono = empresa.Telefono,
                                           Ruc = empresa.Ruc,
                                           Email = empresa.Email,
                                           IdDepartamento = _context.Ciudads.Where(x => x.IdCiudad == empresa.IdCiudad).FirstOrDefault().IdDepartamento,
                                           IdCiudad = empresa.IdCiudad,
                                           Direccion = empresa.Direccion,
                                           NombreRepresentante = empresa.NombreRepresentante,
                                           CelularRepresentante = empresa.CelularRepresentante,
                                           IdPropietario = empresa.IdPropietario,
                                           EsLead = empresa.EsLead
                                       }).FirstOrDefault();

                respuesta.TipoContacto = Defs.CLIENTE_PJ;
            }

            return respuesta;
        }

        private string ObtenerTipoCliente(InfoContacto contacto)
        {
            string tipoContacto;
            if (contacto.TipoContacto == Defs.CLIENTE_PF)
            {
                tipoContacto = ((ContactoAsociadoDTO)contacto.DatosContacto).EsLead ? Defs.CLIENTE_LEAD : Defs.CLIENTE_EXISTENTE;
            }
            else
            {
                tipoContacto = ((EmpresaAsociadaDTO)contacto.DatosContacto).EsLead ? Defs.CLIENTE_LEAD : Defs.CLIENTE_EXISTENTE;
            }

            return tipoContacto;
        }

        public OportunidadDTO ObtenerOportunidadById(int id)
        {
            return null;
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
                FechaCierre = oportunidadlNueva.FechaCierre,
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

        public List<DetalleContactoDTO> ObtenerListaContactos()
        {
            List<DetalleContactoDTO> contactos = (from contacto in _context.Contactos
                                                   where contacto.Estado && !contacto.EsLead
                                                   select new DetalleContactoDTO()
                                                   {
                                                       IdContacto = Defs.CLIENTE_PF + "-" + contacto.IdContacto,
                                                       Nombre = contacto.Nombres + " " + contacto.Apellidos
                                                   }).ToList();

            List<DetalleContactoDTO> empresas = (from empresa in _context.Empresas
                                                 where empresa.Estado && !empresa.EsLead
                                                 select new DetalleContactoDTO()
                                                 {
                                                     IdContacto = Defs.CLIENTE_PJ + "-" + empresa.IdEmpresa,
                                                     Nombre = empresa.Nombre
                                                 }).ToList();

            List<DetalleContactoDTO> resultado = contactos.Union(empresas).ToList();

            return resultado;
        }

        public List<EtapaDTO> ObtenerEtapas()
        {
            List<EtapaDTO> etapas = (from etapa in _context.Etapas
                                     select new EtapaDTO()
                                     {
                                         IdEtapa = etapa.IdEtapa,
                                         Etapa = etapa.Descripcion
                                     }).ToList();
            return etapas;
        }
    }
}
