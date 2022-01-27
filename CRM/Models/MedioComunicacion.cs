using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class MedioComunicacion
    {
        public MedioComunicacion()
        {
            Comunicacions = new HashSet<Comunicacion>();
        }

        public int IdMedioComunicacion { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Comunicacion> Comunicacions { get; set; }
    }
}
