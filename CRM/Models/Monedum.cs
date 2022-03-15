using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Monedum
    {
        public Monedum()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdMoneda { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
