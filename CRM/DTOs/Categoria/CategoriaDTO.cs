using System;

namespace CRM.DTOs.CategoriaDto
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public int? IdCategoriaPadre { get; set; }
        public bool Estado { get; set; }
    } 
}
