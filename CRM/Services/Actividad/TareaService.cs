using CRM.DTOs.Actividad;
using CRM.Helpers;
using CRM.Models;
using System;
using System.Collections.Generic;
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
            return new List<ListaTareaDTO>
            {
                new ListaTareaDTO
                {
                    IdTarea = 1,
                    Titulo = "Nueva Tarea",
                    FechaInicio = DateTime.Now,
                    FechaCierre = DateTime.Now,
                    Estado = "Abierto",
                    Tipo = "Llamada",
                    Responsable = "Elias Mercado"
                }
            };
        }

        public TareaDTO ObtenerTareaById(int idTarea)
        {
            TareaDTO tarea = new TareaDTO
            {
                IdTarea = 1,
                Titulo = "Nueva Tarea",
                Descripcion = "Desc Nueva Tarea",
                FechaInicio = DateTime.Now,
                FechaCierre = DateTime.Now,
                IdTipo = 1,
                IdEstado = 1,
                IdResponsable = 1,
                IdContactoAsociado = 9
            };

            if (tarea.IdContactoAsociado != null)
                tarea.AsociarCon = Defs.CONTACTO;
            else if (tarea.IdEmpresaAsociada != null)
                tarea.AsociarCon = Defs.EMPRESA;
            else if (tarea.IdContactoAsociado != null)
                tarea.AsociarCon = Defs.OPORTUNIDAD;

            return tarea;
        }

        public List<TipoTareaDTO> ObtenerTiposTarea()
        {
            return new List<TipoTareaDTO>
            {
                new TipoTareaDTO
                {
                    IdTipoTarea = 1,
                    TipoTarea = "Llamada"
                },
                new TipoTareaDTO
                {
                    IdTipoTarea = 2,
                    TipoTarea = "Correo"
                }

            };
        }

        public List<ListaTareaCalendarioDTO> ObtenerListaTareasParaCalendario()
        {
            return new List<ListaTareaCalendarioDTO>
            {
                //Vamos a pasar status number 1 cuando es tarea abierta y 0 tarea cerrado,
                //esto es para poner colores en el calendario del Front.
                //en todos los casos vamos a enviar allday = true porque no usamos Horas.
                new ListaTareaCalendarioDTO
                {
                    IdTarea = 1,
                    Text = "Nueva tarea",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    StatusNumber = 1,
                    AllDay = true
                },
                new ListaTareaCalendarioDTO
                {
                    IdTarea = 1,
                    Text = "Nueva tarea2",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(2),
                    StatusNumber = 0,
                    AllDay = true
                }
            };
        }

        public List<EstadoTareaDTO> ObtenerEstadosTarea()
        {
            return new List<EstadoTareaDTO>
            {
                new EstadoTareaDTO
                {
                    IdEstadoTarea = 1,
                    EstadoTarea = "Abierta"
                },
                new EstadoTareaDTO
                {
                    IdEstadoTarea =2,
                    EstadoTarea ="Cerrada"
                }
            };
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

            List<OportunidadDTO> oportunidades= (from oportunidad in _context.Oportunidads
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
