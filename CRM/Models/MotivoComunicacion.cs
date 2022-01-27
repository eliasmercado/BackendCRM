using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class MotivoComunicacion
    {
        public MotivoComunicacion()
        {
            Comunicacions = new HashSet<Comunicacion>();
        }

        public int IdMotivoComunicacion { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Comunicacion> Comunicacions { get; set; }
    }
}
