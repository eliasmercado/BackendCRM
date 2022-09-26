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

        /// <summary>
        /// Obtiene la lista de oportunidades que se va mostrar en la grilla
        /// </summary>
        /// <returns></returns>
        public List<ListaOportunidadDTO> ObtenerListaOportunidades()
        {
            List<Oportunidad> listaOportunidades = _context.Oportunidads.ToList();
            List<ListaOportunidadDTO> listaOportunidadesDto = new();
            ListaOportunidadDTO oportunidadDto;
            InfoContacto contactoAsociado = new();

            foreach (var oportunidad in listaOportunidades)
            {
                oportunidadDto = new();
                oportunidadDto.IdOportunidad = oportunidad.IdOportunidad;
                oportunidadDto.Nombre = oportunidad.Nombre;
                oportunidadDto.FechaCierre = oportunidad.FechaCierre;
                oportunidadDto.FechaCreacion = oportunidad.FechaCreacion;
                oportunidadDto.Etapa = (from etapa in _context.Etapas where etapa.IdEtapa == oportunidad.IdEtapa select etapa.Descripcion).FirstOrDefault();
                oportunidadDto.Prioridad = (from prioridad in _context.Prioridads where prioridad.IdPrioridad == oportunidad.IdPrioridad select prioridad.Descripcion).FirstOrDefault();
                oportunidadDto.Valor = oportunidad.Valor;
                oportunidadDto.Propietario = (from propietario in _context.Usuarios where propietario.IdUsuario == oportunidad.IdPropietario select propietario.Nombres + " " + propietario.Apellidos).FirstOrDefault();

                contactoAsociado = ObtenerContacto(oportunidad);
                oportunidadDto.TipoCliente = ObtenerTipoCliente(contactoAsociado);

                if (contactoAsociado.TipoContacto == Defs.CLIENTE_PF)
                    oportunidadDto.Contacto = ((ContactoAsociadoDTO)contactoAsociado.DatosContacto).Nombres + " " + ((ContactoAsociadoDTO)contactoAsociado.DatosContacto).Apellidos;
                else
                    oportunidadDto.Contacto = ((EmpresaAsociadaDTO)contactoAsociado.DatosContacto).Nombre;

                listaOportunidadesDto.Add(oportunidadDto);
            }

            return listaOportunidadesDto.OrderByDescending(x => x.FechaCreacion).ToList();
        }

        /// <summary>
        /// Obtiene la oportunidad por ID, tiene en cuenta si es cliente PF o Pj,
        /// cliente existente o Lead para retornar los datos.
        /// </summary>
        /// <returns></returns>
        public OportunidadDTO ObtenerOportunidadById(int idOportunidad)
        {
            Oportunidad oportunidad = _context.Oportunidads.Where(x => x.IdOportunidad == idOportunidad).FirstOrDefault();

            if (oportunidad == null)
                throw new ApiException("No se encontraron datos para el id " + idOportunidad);

            OportunidadDTO oportunidadDto = new();
            InfoContacto contacto = new();

            oportunidadDto.IdOportunidad = oportunidad.IdOportunidad;
            oportunidadDto.Nombre = oportunidad.Nombre;
            oportunidadDto.FechaCierre = oportunidad.FechaCierre;
            oportunidadDto.IdEtapa = oportunidad.IdEtapa;
            oportunidadDto.Valor = oportunidad.Valor;
            oportunidadDto.IdPrioridad = oportunidad.IdPrioridad;
            oportunidadDto.IdFuente = oportunidad.IdFuente;
            oportunidadDto.IdSucursal = oportunidad.IdSucursal;

            //Obtenemos los datos del contacto, manejamos si es LEAD o Cliente, PF o PJ
            contacto = ObtenerContacto(oportunidad);

            //Tipo de Cliente: Lead o Existente
            oportunidadDto.TipoCliente = ObtenerTipoCliente(contacto);

            if (contacto.TipoContacto == Defs.CLIENTE_PF)
                oportunidadDto.IdContactoAsociado = Defs.CLIENTE_PF + "-" + ((ContactoAsociadoDTO)contacto.DatosContacto).IdContacto;
            else
                oportunidadDto.IdContactoAsociado = Defs.CLIENTE_PJ + "-" + ((EmpresaAsociadaDTO)contacto.DatosContacto).IdEmpresa;

            oportunidadDto.Detalles = (from detalle in _context.DetalleOportunidads
                                       where detalle.IdOportunidad == oportunidad.IdOportunidad
                                       select new DetalleOportunidadDTO()
                                       {
                                           IdDetalleOportunidad = detalle.IdDetalleOportunidad,
                                           IdProducto = detalle.IdProducto,
                                           Cantidad = detalle.Cantidad
                                       }).ToList();

            oportunidadDto.IdPropietario = oportunidad.IdPropietario;
            oportunidadDto.Observacion = oportunidad.Observacion;

            return oportunidadDto;
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

        public List<DetalleContactoDTO> ObtenerListaContactos(bool esLead)
        {
            List<DetalleContactoDTO> contactos = (from contacto in _context.Contactos
                                                  where contacto.Estado && contacto.EsLead == esLead
                                                  select new DetalleContactoDTO()
                                                  {
                                                      IdContacto = Defs.CLIENTE_PF + "-" + contacto.IdContacto,
                                                      Nombre = contacto.Nombres + " " + contacto.Apellidos
                                                  }).ToList();

            List<DetalleContactoDTO> empresas = (from empresa in _context.Empresas
                                                 where empresa.Estado && empresa.EsLead == esLead
                                                 select new DetalleContactoDTO()
                                                 {
                                                     IdContacto = Defs.CLIENTE_PJ + "-" + empresa.IdEmpresa,
                                                     Nombre = empresa.Nombre
                                                 }).ToList();

            List<DetalleContactoDTO> resultado = contactos.Union(empresas).ToList();

            return resultado;
        }

        private InfoContacto ObtenerContacto(Oportunidad oportunidad)
        {
            InfoContacto respuesta = new();

            //Si es un cliente pf
            if (oportunidad.IdEmpresa == null)
            {
                ContactoAsociadoDTO contacto = (from contact in _context.Contactos
                                                where contact.Estado && contact.IdContacto == oportunidad.IdContacto
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

                respuesta.DatosContacto = contacto;
                respuesta.TipoContacto = Defs.CLIENTE_PF;
            }
            //si es cliente pj
            else
            {
                EmpresaAsociadaDTO contacto = (from empresa in _context.Empresas
                                               where empresa.Estado && empresa.IdEmpresa == oportunidad.IdEmpresa
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

                respuesta.DatosContacto = contacto;
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

        public List<SelectSucursalDTO> ObtenerSucursales()
        {
            List<SelectSucursalDTO> sucursales = (from sucursal in _context.Sucursals
                                                  select new SelectSucursalDTO()
                                                  {
                                                      IdSucursal = sucursal.IdSucursal,
                                                      Sucursal = sucursal.Descripcion
                                                  }).ToList();
            return sucursales;
        }

        public List<FuenteDTO> ObtenerFuentes()
        {
            List<FuenteDTO> fuentes = (from fuente in _context.Fuentes
                                                  select new FuenteDTO()
                                                  {
                                                      IdFuente = fuente.IdFuente,
                                                      Fuente = fuente.Descripcion
                                                  }).ToList();
            return fuentes;
        }
    }
}
