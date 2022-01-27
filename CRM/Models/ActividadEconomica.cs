using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class ActividadEconomica
    {
        public ActividadEconomica()
        {
            Contactos = new HashSet<Contacto>();
        }

        public int IdActividadEconomica { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; }
    }
}
