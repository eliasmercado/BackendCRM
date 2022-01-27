using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class FuenteTicket
    {
        public FuenteTicket()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int IdFuenteTicket { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
