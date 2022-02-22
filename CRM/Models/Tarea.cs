using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Tarea
    {
        public Tarea()
        {
            InverseIdTareaAsociadaNavigation = new HashSet<Tarea>();
        }

        public int IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int IdEstadoActividad { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaCierre { get; set; }
        public int IdTipoTarea { get; set; }
        public int? IdRecordatorio { get; set; }
        public int? IdContacto { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTicket { get; set; }
        public int? IdTareaAsociada { get; set; }

        public virtual Contacto IdContactoNavigation { get; set; }
        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual EstadoActividad IdEstadoActividadNavigation { get; set; }
        public virtual Oportunidad IdOportunidadNavigation { get; set; }
        public virtual Recordatorio IdRecordatorioNavigation { get; set; }
        public virtual Tarea IdTareaAsociadaNavigation { get; set; }
        public virtual Ticket IdTicketNavigation { get; set; }
        public virtual TipoTarea IdTipoTareaNavigation { get; set; }
        public virtual Usuario IdUsuarioResponsableNavigation { get; set; }
        public virtual ICollection<Tarea> InverseIdTareaAsociadaNavigation { get; set; }
    }
}
