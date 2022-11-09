using CRM.DTOs.Oportunidad;
using CRM.Helpers;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            List<Oportunidad> listaOportunidades = _context.Oportunidads.Where(x => x.IdEtapaNavigation.Descripcion != Defs.OPORTUNIDAD_CANCELADA).ToList();
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
                oportunidadDto.Valor = "Gs " + oportunidad.Valor.ToString("N0", new CultureInfo("es-PY"));
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

        public OportunidadInfoDTO ObtenerOportunidadInfo(int idOportunidad)
        {
            Oportunidad oportunidad = _context.Oportunidads.Find(idOportunidad);
            OportunidadInfoDTO oportunidadInfo = new();

            if (oportunidad == null)
                throw new ApiException("La oportunidad no existe");

            oportunidadInfo.IdOportunidad = oportunidad.IdOportunidad;
            oportunidadInfo.Nombre = oportunidad.Nombre;
            oportunidadInfo.FechaCierre = oportunidad.FechaCierre.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            oportunidadInfo.Etapa = _context.Etapas.Where(x => x.IdEtapa == oportunidad.IdEtapa).Select(x => x.Descripcion).FirstOrDefault();
            oportunidadInfo.Valor = "Gs " + oportunidad.Valor.ToString("N0", new CultureInfo("es-PY"));

            InfoContacto contacto = new();
            //Obtenemos los datos del contacto, manejamos si es LEAD o Cliente, PF o PJ
            contacto = ObtenerContacto(oportunidad);

            //Tipo de Cliente: Lead o Existente
            oportunidadInfo.TipoCliente = ObtenerTipoCliente(contacto);
            oportunidadInfo.Prioridad = _context.Prioridads.Where(x => x.IdPrioridad == oportunidad.IdPrioridad).Select(x => x.Descripcion).FirstOrDefault();
            oportunidadInfo.Fuente = _context.Fuentes.Where(x => x.IdFuente == oportunidad.IdFuente).Select(x => x.Descripcion).FirstOrDefault();
            oportunidadInfo.Sucursal = _context.Sucursals.Where(x => x.IdSucursal == oportunidad.IdSucursal).Select(x => x.Descripcion).FirstOrDefault();
            oportunidadInfo.Observacion = oportunidad.Observacion;
            oportunidadInfo.Propietario = _context.Usuarios.Where(x => x.IdUsuario == oportunidad.IdPropietario).Select(x => x.Nombres + " " + x.Apellidos).FirstOrDefault();

            if (contacto.TipoContacto == Defs.CLIENTE_PF)
            {
                oportunidadInfo.ContactoAsociado = new();
                ContactoAsociadoDTO contactoPf = (ContactoAsociadoDTO)contacto.DatosContacto;
                oportunidadInfo.ContactoAsociado.IdContacto = contactoPf.IdContacto;
                oportunidadInfo.ContactoAsociado.NombreCompleto = contactoPf.Nombres + " " + contactoPf.Apellidos;
                oportunidadInfo.ContactoAsociado.Celular = contactoPf.Celular;
                oportunidadInfo.ContactoAsociado.Email = contactoPf.Email;
                oportunidadInfo.TipoContacto = Defs.CONTACTO;
            }
            else
            {
                oportunidadInfo.EmpresaAsociada = new();
                EmpresaAsociadaDTO contactoPj = (EmpresaAsociadaDTO)contacto.DatosContacto;
                oportunidadInfo.EmpresaAsociada.IdEmpresa = contactoPj.IdEmpresa;
                oportunidadInfo.EmpresaAsociada.Nombre = contactoPj.Nombre;
                oportunidadInfo.EmpresaAsociada.Celular = contactoPj.Celular;
                oportunidadInfo.EmpresaAsociada.Telefono = contactoPj.Telefono;
                oportunidadInfo.EmpresaAsociada.Email = contactoPj.Email;
                oportunidadInfo.TipoContacto = Defs.EMPRESA;
            }

            List<DetalleOportunidad> detalles = _context.DetalleOportunidads.Where(x => x.IdOportunidad == oportunidad.IdOportunidad).ToList();

            oportunidadInfo.Detalles = new();
            foreach (var detalleOp in detalles)
            {
                oportunidadInfo.Detalles.Add(new DetalleOportunidadDTO()
                {
                    IdDetalleOportunidad = detalleOp.IdDetalleOportunidad,
                    IdProducto = detalleOp.IdProducto,
                    Producto = _context.Productos.Find(detalleOp.IdProducto).Descripcion,
                    Cantidad = detalleOp.Cantidad
                });
            }

            return oportunidadInfo;
        }

        public string CrearOportunidad(OportunidadDTO oportunidadNueva)
        {
            //para identificar si es un contacto o empresa.
            bool esPF = true;

            Oportunidad oportunidad = new();
            oportunidad.Nombre = oportunidadNueva.Nombre;
            oportunidad.IdEtapa = oportunidadNueva.IdEtapa;
            oportunidad.FechaCreacion = DateTime.Now;
            oportunidad.FechaCierre = oportunidadNueva.FechaCierre;
            oportunidad.IdPrioridad = oportunidadNueva.IdPrioridad;

            //para obtener el idContacto asociado hacemos un split
            //el primer elemento es el tipo de cliente y el segundo el ID.
            string[] elementos = oportunidadNueva.IdContactoAsociado.Split('-');
            int idContactoAsociado = int.Parse(elementos[1]);

            if (elementos[0] == Defs.CLIENTE_PF)
            {
                oportunidad.IdContacto = int.Parse(elementos[1]);
                oportunidad.IdEmpresa = null;
                esPF = true;
            }
            else
            {
                oportunidad.IdEmpresa = int.Parse(elementos[1]);
                oportunidad.IdContacto = null;
                esPF = false;
            }
            oportunidad.IdFuente = oportunidadNueva.IdFuente;
            oportunidad.Observacion = string.IsNullOrEmpty(oportunidadNueva.Observacion) ? null : oportunidadNueva.Observacion;
            oportunidad.IdSucursal = oportunidadNueva.IdSucursal;
            oportunidad.IdPropietario = oportunidadNueva.IdPropietario;

            Producto producto;
            foreach (DetalleOportunidadDTO detalle in oportunidadNueva.Detalles)
            {
                producto = _context.Productos.Find(detalle.IdProducto);
                oportunidad.Valor += detalle.Cantidad * producto.Precio;
            }

            DetalleOportunidad detalleOportunidad;
            foreach (DetalleOportunidadDTO detalle in oportunidadNueva.Detalles)
            {
                detalleOportunidad = new();
                detalleOportunidad.IdProducto = detalle.IdProducto;
                detalleOportunidad.Cantidad = detalle.Cantidad;
                oportunidad.DetalleOportunidads.Add(detalleOportunidad);
            }

            //Verificamos si la etapa es cerrada ganada, en caso de que el cliente sea Lead va pasar a ser un contacto
            Etapa etapa = _context.Etapas.Find(oportunidadNueva.IdEtapa);

            if (etapa.Descripcion == Defs.OPORTUNIDAD_GANADA)
            {
                if (esPF)
                {
                    Contacto contacto = _context.Contactos.Where(x => x.IdContacto == idContactoAsociado).FirstOrDefault();
                    contacto.EsLead = false;
                    _context.Entry(contacto).State = EntityState.Modified;
                }
                else
                {
                    Empresa empresa = _context.Empresas.Where(x => x.IdEmpresa == idContactoAsociado).FirstOrDefault();
                    empresa.EsLead = false;
                    _context.Entry(empresa).State = EntityState.Modified;
                }
            }

            _context.Oportunidads.Add(oportunidad);
            _context.SaveChanges();

            return "La oportunidad se agregó correctamente";
        }

        public string ModificarOportunidad(int id, OportunidadDTO oportunidadModificada)
        {
            if (id != oportunidadModificada.IdOportunidad)
            {
                throw new ApiException("Identificador de Oportunidad no válido");
            }

            //para identificar si es un contacto o empresa.
            bool esPF = true;

            Oportunidad oportunidad = _context.Oportunidads.Find(id);

            if (oportunidad == null)
                throw new ApiException("La oportunidad no existe");

            oportunidad.Nombre = oportunidadModificada.Nombre;
            oportunidad.IdEtapa = oportunidadModificada.IdEtapa;
            oportunidad.FechaCierre = oportunidadModificada.FechaCierre;
            oportunidad.IdPrioridad = oportunidadModificada.IdPrioridad;

            //para obtener el idContacto asociado hacemos un split
            //el primer elemento es el tipo de cliente y el segundo el ID.
            string[] elementos = oportunidadModificada.IdContactoAsociado.Split('-');
            int idContactoAsociado = int.Parse(elementos[1]);

            if (elementos[0] == Defs.CLIENTE_PF)
            {
                oportunidad.IdContacto = int.Parse(elementos[1]);
                oportunidad.IdEmpresa = null;
                esPF = true;
            }
            else
            {
                oportunidad.IdEmpresa = int.Parse(elementos[1]);
                oportunidad.IdContacto = null;
                esPF = false;
            }

            oportunidad.IdFuente = oportunidadModificada.IdFuente;
            oportunidad.Observacion = string.IsNullOrEmpty(oportunidadModificada.Observacion) ? null : oportunidadModificada.Observacion;
            oportunidad.IdSucursal = oportunidadModificada.IdSucursal;
            oportunidad.IdPropietario = oportunidadModificada.IdPropietario;

            Producto producto;
            oportunidad.Valor = 0;
            foreach (DetalleOportunidadDTO detalle in oportunidadModificada.Detalles)
            {
                producto = _context.Productos.Find(detalle.IdProducto);
                oportunidad.Valor += detalle.Cantidad * producto.Precio;
            }

            DetalleOportunidad detalleOportunidad;
            List<DetalleOportunidad> detallesOportunidad = _context.DetalleOportunidads.Where(x => x.IdOportunidad == oportunidadModificada.IdOportunidad).ToList();
            List<int> idDetallesActualizados = new();
            foreach (DetalleOportunidadDTO detalle in oportunidadModificada.Detalles)
            {
                detalleOportunidad = new();
                DetalleOportunidad detalleActual = detallesOportunidad.Find(x => x.IdDetalleOportunidad == detalle.IdDetalleOportunidad);

                //si no existe vamos a insertar, si existe lo actualizamos
                if (detalleActual == null)
                {
                    detalleOportunidad.IdProducto = detalle.IdProducto;
                    detalleOportunidad.Cantidad = detalle.Cantidad;
                    oportunidad.DetalleOportunidads.Add(detalleOportunidad);
                }
                else
                {
                    detalleActual.IdProducto = detalle.IdProducto;
                    detalleActual.Cantidad = detalle.Cantidad;
                    _context.Entry(detalleActual).State = EntityState.Modified;
                }
                idDetallesActualizados.Add(detalle.IdDetalleOportunidad);
            }

            //Si existen registros que ya no vinieron en el detalle tenemos que borrar de la BD
            foreach (DetalleOportunidad detalleOp in detallesOportunidad)
            {
                if (!idDetallesActualizados.Contains(detalleOp.IdDetalleOportunidad))
                    _context.Remove(detalleOp);
            }

            //Verificamos si la etapa es cerrada ganada, en caso de que el cliente sea Lead va pasar a ser un contacto
            Etapa etapa = _context.Etapas.Find(oportunidadModificada.IdEtapa);

            if (etapa.Descripcion == Defs.OPORTUNIDAD_GANADA)
            {
                if (esPF)
                {
                    Contacto contacto = _context.Contactos.Where(x => x.IdContacto == idContactoAsociado).FirstOrDefault();
                    contacto.EsLead = false;
                    _context.Entry(contacto).State = EntityState.Modified;
                }
                else
                {
                    Empresa empresa = _context.Empresas.Where(x => x.IdEmpresa == idContactoAsociado).FirstOrDefault();
                    empresa.EsLead = false;
                    _context.Entry(empresa).State = EntityState.Modified;
                }
            }

            _context.SaveChanges();

            return "La Oportunidad se modificó correctamente";
        }

        public string ModificarEtapaOportunidad(int id, string etapa)
        {
            Oportunidad oportunidad = _context.Oportunidads.Find(id);

            if (oportunidad == null)
                throw new ApiException("La oportunidad no existe");

            int idEtapa = _context.Etapas.Where(x => x.Descripcion == etapa).FirstOrDefault().IdEtapa;

            oportunidad.IdEtapa = idEtapa;

            _context.SaveChanges();

            return "La etapa se modificó correctamente";
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

        public InfoContacto ObtenerContacto(Oportunidad oportunidad)
        {
            InfoContacto respuesta = new();

            //Si es un cliente pf
            if (oportunidad.IdEmpresa == null)
            {
                ContactoAsociadoDTO contacto = (from contact in _context.Contactos
                                                where contact.IdContacto == oportunidad.IdContacto
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
                                               where empresa.IdEmpresa == oportunidad.IdEmpresa
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
