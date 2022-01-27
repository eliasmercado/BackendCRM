using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class EstadoCivil
    {
        public EstadoCivil()
        {
            Contactos = new HashSet<Contacto>();
        }

        public int IdEstadoCivil { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; }
    }
}
