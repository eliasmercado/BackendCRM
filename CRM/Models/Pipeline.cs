using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Pipeline
    {
        public Pipeline()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int IdPipeline { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
