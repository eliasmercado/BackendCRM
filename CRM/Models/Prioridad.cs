using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Prioridad
    {
        public Prioridad()
        {
            Oportunidads = new HashSet<Oportunidad>();
            Tickets = new HashSet<Ticket>();
        }

        public int IdPrioridad { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Oportunidad> Oportunidads { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
