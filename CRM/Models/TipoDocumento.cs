using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            Contactos = new HashSet<Contacto>();
        }

        public int IdTipoDocumento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; }
    }
}
