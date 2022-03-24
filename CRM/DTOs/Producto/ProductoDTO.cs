using System;

namespace CRM.DTOs.Producto
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoriaPadre { get; set; }
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
    }

    public class MonedaDTO
    {
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public string Codigo { get; set; }
    }
}
