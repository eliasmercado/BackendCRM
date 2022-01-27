using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Fuente
    {
        public Fuente()
        {
            Oportunidads = new HashSet<Oportunidad>();
        }

        public int IdFuente { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Oportunidad> Oportunidads { get; set; }
    }
}
