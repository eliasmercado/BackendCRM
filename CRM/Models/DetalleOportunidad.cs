using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class DetalleOportunidad
    {
        public int IdDetalleOportunidad { get; set; }
        public int IdProducto { get; set; }
        public int IdOportunidad { get; set; }
        public int Cantidad { get; set; }

        public virtual Oportunidad IdOportunidadNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
