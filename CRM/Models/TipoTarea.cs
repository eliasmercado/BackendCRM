using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class TipoTarea
    {
        public TipoTarea()
        {
            Tareas = new HashSet<Tarea>();
        }

        public int IdTipoTarea { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
