using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Recordatorio
    {
        public Recordatorio()
        {
            Tareas = new HashSet<Tarea>();
        }

        public int IdRecordatorio { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
