using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Oportunidad
    {
        public Oportunidad()
        {
            DetalleOportunidads = new HashSet<DetalleOportunidad>();
            Pedidos = new HashSet<Pedido>();
            Tareas = new HashSet<Tarea>();
        }

        public int IdOportunidad { get; set; }
        public string Nombre { get; set; }
        public int IdEtapa { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaCierre { get; set; }
        public int IdPrioridad { get; set; }
        public int? IdContacto { get; set; }
        public int? IdEmpresa { get; set; }
        public int IdFuente { get; set; }
        public string Observacion { get; set; }
        public int IdSucursal { get; set; }
        public int IdPropietario { get; set; }

        public virtual Contacto IdContactoNavigation { get; set; }
        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual Etapa IdEtapaNavigation { get; set; }
        public virtual Fuente IdFuenteNavigation { get; set; }
        public virtual Prioridad IdPrioridadNavigation { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
        public virtual ICollection<DetalleOportunidad> DetalleOportunidads { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
