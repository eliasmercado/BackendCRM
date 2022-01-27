using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Etapa
    {
        public Etapa()
        {
            Oportunidads = new HashSet<Oportunidad>();
        }

        public int IdEtapa { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Oportunidad> Oportunidads { get; set; }
    }
}
