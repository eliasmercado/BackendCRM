using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Tareas = new HashSet<Tarea>();
        }

        public int IdTicket { get; set; }
        public string Nombre { get; set; }
        public string Decripcion { get; set; }
        public int IdEstadoActividad { get; set; }
        public int IdPipeline { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public int IdFuenteTicket { get; set; }
        public int IdPrioridad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaCierre { get; set; }
        public int? IdContacto { get; set; }
        public int? IdEmpresa { get; set; }

        public virtual Contacto IdContactoNavigation { get; set; }
        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual EstadoActividad IdEstadoActividadNavigation { get; set; }
        public virtual FuenteTicket IdFuenteTicketNavigation { get; set; }
        public virtual Pipeline IdPipelineNavigation { get; set; }
        public virtual Prioridad IdPrioridadNavigation { get; set; }
        public virtual Usuario IdUsuarioResponsableNavigation { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
