using System;

namespace CRM.DTOs.Producto
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdCategoriaPadre { get; set; }
        public bool Estado { get; set; }
    } 
}
