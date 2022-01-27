using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class EstadoActividad
    {
        public EstadoActividad()
        {
            Tareas = new HashSet<Tarea>();
            Tickets = new HashSet<Ticket>();
        }

        public int IdEstadoActividad { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Tarea> Tareas { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
