using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Oportunidads = new HashSet<Oportunidad>();
        }

        public int IdSucursal { get; set; }
        public string Descripcion { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; }

        public virtual Ciudad IdCiudadNavigation { get; set; }
        public virtual ICollection<Oportunidad> Oportunidads { get; set; }
    }
}
