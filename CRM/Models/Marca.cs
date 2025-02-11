﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdMarca { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
