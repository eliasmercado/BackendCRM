using CRM.DTOs.Actividad;
using CRM.Helpers;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Actividad
{
    public class TareaService
    {
        private CrmDbContext _context;

        public TareaService(CrmDbContext context)
        {
            _context = context;
        }

        public List<ListaTareaDTO> ObtenerListaTareas()
        {
            List<ListaTareaDTO> tareas = (from tarea in _context.Tareas
                                          select new ListaTareaDTO
                                          {
                                              IdTarea = tarea.IdTarea,
                                              Titulo = tarea.Titulo,
                                              FechaInicio = tarea.FechaInicio,
                                              FechaCierre = tarea.FechaCierre,
                                              FechaCreacion = tarea.FechaCreacion,
                                              Estado = _context.EstadoActividads.Where(x => x.IdEstadoActividad == tarea.IdEstadoActividad).Select(x => x.Descripcion).FirstOrDefault(),
                                              Tipo = _context.TipoTareas.Where(x => x.IdTipoTarea == tarea.IdTipoTarea).Select(x => x.Descripcion).FirstOrDefault(),
                                              Responsable = _context.Usuarios.Where(x => x.IdUsuario == tarea.IdUsuarioResponsable).Select(x => x.Nombres + " " + x.Apellidos).FirstOrDefault()
                                          }).ToList();

            return tareas.OrderBy(x => x.FechaInicio).OrderBy(x => x.Estado).ToList();
        }

        public TareaDTO ObtenerTareaById(int idTarea)
        {
            Tarea tarea = _context.Tareas.Find(idTarea);

            if (tarea == null)
                throw new ApiException("La Tarea no existe");

            TareaDTO tareaResponse = new TareaDTO
            {
                IdTarea = tarea.IdTarea,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                FechaInicio = tarea.FechaInicio,
                FechaCierre = tarea.FechaCierre,
                IdTipo = tarea.IdTipoTarea,
                IdEstado = tarea.IdEstadoActividad,
                IdResponsable = tarea.IdUsuarioResponsable,
                IdContactoAsociado = tarea.IdContacto,
                IdEmpresaAsociada = tarea.IdEmpresa,
                IdOportunidadAsociada = tarea.IdOportunidad
            };

            if (tareaResponse.IdContactoAsociado != null)
                tareaResponse.AsociarCon = Defs.CONTACTO;
            else if (tareaResponse.IdEmpresaAsociada != null)
                tareaResponse.AsociarCon = Defs.EMPRESA;
            else if (tareaResponse.IdOportunidadAsociada != null)
                tareaResponse.AsociarCon = Defs.OPORTUNIDAD;

            return tareaResponse;
        }

        public string CrearTarea(TareaDTO tareaNueva)
        {
            Tarea tarea = new()
            {
                Titulo = tareaNueva.Titulo,
                Descripcion = tareaNueva.Descripcion,
                IdTipoTarea = tareaNueva.IdTipo,
                IdEstadoActividad = tareaNueva.IdEstado,
                FechaInicio = tareaNueva.FechaInicio,
                FechaCierre = tareaNueva.FechaCierre,
                IdUsuarioResponsable = tareaNueva.IdResponsable
            };

            if (tareaNueva.AsociarCon == Defs.CONTACTO)
                tarea.IdContacto = tareaNueva.IdContactoAsociado;
            else if (tareaNueva.AsociarCon == Defs.EMPRESA)
                tarea.IdEmpresa = tareaNueva.IdEmpresaAsociada;
            else if (tareaNueva.AsociarCon == Defs.OPORTUNIDAD)
                tarea.IdOportunidad = tareaNueva.IdOportunidadAsociada;

            tarea.FechaCreacion = DateTime.Now;

            _context.Tareas.Add(tarea);
            _context.SaveChanges();

            return "La tarea se agregó correctamente";
        }

        public string CerrarTarea(int idTarea)
        {
            Tarea tarea = _context.Tareas.Find(idTarea);

            if (tarea == null)
                throw new ApiException("La tarea no existe");

            int idEstado = _context.EstadoActividads.Where(x => x.Descripcion == Defs.TAREA_CERRADA).FirstOrDefault().IdEstadoActividad;

            tarea.IdEstadoActividad = idEstado;

            _context.SaveChanges();

            return "La tarea se cerró correctamente";
        }

        public string ModificarTarea(int id, TareaDTO tareaModificada)
        {
            if (id != tareaModificada.IdTarea)
            {
                throw new ApiException("Identificador de Tarea no válido");
            }

            Tarea tarea = _context.Tareas.Find(id);

            if (tarea == null)
                throw new ApiException("La Tarea no existe");

            tarea.Titulo = tareaModificada.Titulo;
            tarea.Descripcion = tareaModificada.Descripcion;
            tarea.IdTipoTarea = tareaModificada.IdTipo;
            tarea.IdEstadoActividad = tareaModificada.IdEstado;
            tarea.FechaInicio = tareaModificada.FechaInicio;
            tarea.FechaCierre = tareaModificada.FechaCierre;
            tarea.IdUsuarioResponsable = tareaModificada.IdResponsable;

            if (tareaModificada.AsociarCon == Defs.CONTACTO)
            {
                tarea.IdContacto = tareaModificada.IdContactoAsociado;
                tarea.IdEmpresa = null;
                tarea.IdOportunidad = null;
            }
            else if (tareaModificada.AsociarCon == Defs.EMPRESA)
            {
                tarea.IdEmpresa = tareaModificada.IdEmpresaAsociada;
                tarea.IdContacto = null;
                tarea.IdOportunidad = null;
            }
            else if (tareaModificada.AsociarCon == Defs.OPORTUNIDAD)
            {
                tarea.IdOportunidad = tareaModificada.IdOportunidadAsociada;
                tarea.IdEmpresa = null;
                tarea.IdContacto = null;
            }
            else
            {
                tarea.IdOportunidad = null;
                tarea.IdEmpresa = null;
                tarea.IdContacto = null;
            }

            _context.SaveChanges();

            return "La tarea se modificó correctamente";
        }

        public string EliminarTarea(int id)
        {
            Tarea tarea = _context.Tareas.Find(id);

            if (tarea == null)
                throw new ApiException("La tarea no existe");

            _context.Remove(tarea);
            _context.SaveChanges();

            return "La tarea se eliminó correctamente";
        }

        public List<TipoTareaDTO> ObtenerTiposTarea()
        {
            List<TipoTarea> tipoTarea = _context.TipoTareas.ToList();
            List<TipoTareaDTO> tipoTareaResponse = new();

            foreach (var tipo in tipoTarea)
            {
                tipoTareaResponse.Add(new TipoTareaDTO
                {
                    IdTipoTarea = tipo.IdTipoTarea,
                    TipoTarea = tipo.Descripcion
                });
            }

            return tipoTareaResponse;
        }

        public List<ListaTareaCalendarioDTO> ObtenerListaTareasParaCalendario()
        {
            List<Tarea> tareas = _context.Tareas.ToList();
            List<ListaTareaCalendarioDTO> tareasCalendario = new();
            string estadoTarea = null;
            foreach (var tarea in tareas)
            {
                //Vamos a pasar status number 1 cuando es tarea abierta y 0 tarea cerrado,
                //esto es para poner colores en el calendario del Front.
                //en todos los casos vamos a enviar allday = true porque no usamos Horas.
                estadoTarea = _context.EstadoActividads.Find(tarea.IdEstadoActividad).Descripcion;
                tareasCalendario.Add(new ListaTareaCalendarioDTO
                {
                    IdTarea = tarea.IdTarea,
                    Text = tarea.Titulo,
                    StartDate = tarea.FechaInicio,
                    EndDate = tarea.FechaCierre,
                    StatusNumber = estadoTarea == Defs.TAREA_ABIERTA ? 1 : 0,
                    AllDay = true
                });
            }

            return tareasCalendario;
        }

        public TareaInfoDTO ObtenerTareaByIdParaDetalle(int idTarea)
        {
            Tarea tarea = _context.Tareas.Find(idTarea);

            if (tarea == null)
                throw new ApiException("La Tarea no existe");

            TareaInfoDTO tareaResponse = new TareaInfoDTO
            {
                IdTarea = tarea.IdTarea,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                FechaInicio = tarea.FechaInicio.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                FechaCierre = tarea.FechaCierre.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Tipo = _context.TipoTareas.Where(x => x.IdTipoTarea == tarea.IdTipoTarea).Select(x => x.Descripcion).FirstOrDefault(),
                Estado = _context.EstadoActividads.Where(x => x.IdEstadoActividad == tarea.IdEstadoActividad).Select(x => x.Descripcion).FirstOrDefault(),
                Responsable = _context.Usuarios.Where(x => x.IdUsuario == tarea.IdUsuarioResponsable).Select(x => x.Nombres + " " + x.Apellidos).FirstOrDefault()
            };

            if (tarea.IdContacto != null)
            {
                Contacto contactoAsociado = _context.Contactos.Find(tarea.IdContacto);
                tareaResponse.ContactoAsociado = new ContactoInfoDTO
                {
                    IdContacto = contactoAsociado.IdContacto,
                    NombreCompleto = contactoAsociado.Nombres + " " + contactoAsociado.Apellidos,
                    Celular = contactoAsociado.Celular,
                    Email = contactoAsociado.Email
                };
                tareaResponse.AsociarCon = Defs.CONTACTO;
            }
            else if (tarea.IdEmpresa != null)
            {
                Empresa empresaAsociada = _context.Empresas.Find(tarea.IdEmpresa);
                tareaResponse.EmpresaAsociada = new EmpresaInfoDTO
                {
                    IdEmpresa = empresaAsociada.IdEmpresa,
                    Nombre = empresaAsociada.Nombre,
                    Celular = empresaAsociada.Celular,
                    Telefono = empresaAsociada.Telefono,
                    Email = empresaAsociada.Email
                };
                tareaResponse.AsociarCon = Defs.EMPRESA;
            }
            else if (tarea.IdOportunidad != null)
            {
                tareaResponse.AsociarCon = Defs.OPORTUNIDAD;
                Oportunidad oportunidadAsociada = _context.Oportunidads.Find(tarea.IdOportunidad);
                tareaResponse.OportunidadAsociada = new OportunidadInfoDTO
                {
                    IdOportunidad = oportunidadAsociada.IdOportunidad,
                    Nombre = oportunidadAsociada.Nombre,
                    Etapa = _context.Etapas.Where(x => x.IdEtapa == oportunidadAsociada.IdEtapa).Select(x => x.Descripcion).FirstOrDefault(),
                    Valor = "Gs " + oportunidadAsociada.Valor.ToString("N0", new CultureInfo("es-PY")),
                    Contacto = ObtenerContactoDeOportunidad(oportunidadAsociada)
                };
            }
            return tareaResponse;
        }

        private string ObtenerContactoDeOportunidad(Oportunidad oportunidadAsociada)
        {
            //Si es un cliente pf
            if (oportunidadAsociada.IdEmpresa == null)
            {
                Contacto contacto = _context.Contactos.Find(oportunidadAsociada.IdContacto);
                return contacto.Nombres + " " + contacto.Apellidos;
            }
            //si es cliente pj
            else
            {
                return _context.Empresas.Find(oportunidadAsociada.IdEmpresa).Nombre;
            }
        }

        public List<EstadoTareaDTO> ObtenerEstadosTarea()
        {
            List<EstadoActividad> estadoTarea = _context.EstadoActividads.ToList();
            List<EstadoTareaDTO> estadoTareaResponse = new();

            foreach (var estado in estadoTarea)
            {
                estadoTareaResponse.Add(new EstadoTareaDTO
                {
                    IdEstadoTarea = estado.IdEstadoActividad,
                    EstadoTarea = estado.Descripcion
                });
            }

            return estadoTareaResponse;
        }

        public List<ContactoDTO> ObtenerListaContactos()
        {
            List<ContactoDTO> contactos = (from contacto in _context.Contactos
                                           where contacto.Estado && !contacto.EsLead
                                           select new ContactoDTO()
                                           {
                                               IdContacto = contacto.IdContacto,
                                               Nombre = contacto.Nombres + " " + contacto.Apellidos
                                           }).ToList();


            return contactos;
        }

        public List<EmpresaDTO> ObtenerListaEmpresas()
        {
            List<EmpresaDTO> empresas = (from empresa in _context.Empresas
                                         where empresa.Estado && !empresa.EsLead
                                         select new EmpresaDTO()
                                         {
                                             IdEmpresa = empresa.IdEmpresa,
                                             Nombre = empresa.Nombre
                                         }).ToList();


            return empresas;
        }
        public List<OportunidadDTO> ObtenerListaOportunidades()
        {
            int idEtapaCancelada = _context.Etapas.Where(x => x.Descripcion == Defs.OPORTUNIDAD_CANCELADA).FirstOrDefault().IdEtapa;

            List<OportunidadDTO> oportunidades = (from oportunidad in _context.Oportunidads
                                                  where oportunidad.IdEtapa != idEtapaCancelada
                                                  select new OportunidadDTO()
                                                  {
                                                      IdOportunidad = oportunidad.IdOportunidad,
                                                      Nombre = oportunidad.Nombre
                                                  }).ToList();

            return oportunidades;
        }
    }
}
