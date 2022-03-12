using System;

namespace CRM.DTOs.Producto
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public int IdMoneda { get; set; }
    } 
}
