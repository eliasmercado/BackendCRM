using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Menu
    {
        public Menu()
        {
            InverseIdMenuPadreNavigation = new HashSet<Menu>();
        }

        public int IdMenu { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string MenuUrl { get; set; }
        public int OrdenAparicion { get; set; }
        public bool Estado { get; set; }
        public int? IdMenuPadre { get; set; }
        public string Icono { get; set; }

        public virtual Menu IdMenuPadreNavigation { get; set; }
        public virtual ICollection<Menu> InverseIdMenuPadreNavigation { get; set; }
    }
}
