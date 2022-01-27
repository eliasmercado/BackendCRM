using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            Contactos = new HashSet<Contacto>();
            Empresas = new HashSet<Empresa>();
            Pedidos = new HashSet<Pedido>();
            Sucursals = new HashSet<Sucursal>();
        }

        public int IdCiudad { get; set; }
        public string Descripcion { get; set; }
        public int IdDepartamento { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; }
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<Sucursal> Sucursals { get; set; }
    }
}
