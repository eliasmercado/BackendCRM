using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Ciudads = new HashSet<Ciudad>();
        }

        public int IdDepartamento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Ciudad> Ciudads { get; set; }
    }
}
