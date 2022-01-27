using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleOportunidads = new HashSet<DetalleOportunidad>();
        }

        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public int IdMoneda { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual Marca IdMarcaNavigation { get; set; }
        public virtual Monedum IdMonedaNavigation { get; set; }
        public virtual ICollection<DetalleOportunidad> DetalleOportunidads { get; set; }
    }
}
