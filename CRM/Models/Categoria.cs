using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            InverseIdCategoriaPadreNavigation = new HashSet<Categoria>();
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdCategoriaPadre { get; set; }
        public bool Estado { get; set; }

        public virtual Categoria IdCategoriaPadreNavigation { get; set; }
        public virtual ICollection<Categoria> InverseIdCategoriaPadreNavigation { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
