using System;

namespace CRM.DTOs.Producto
{
    public class MarcaDTO
    {
        public int IdMarca { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string Descripcion { get; set; }
    }
}