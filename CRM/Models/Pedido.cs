using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Pedido
    {
        public int IdPedido { get; set; }
        public int IdOportunidad { get; set; }
        public int IdFase { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaCierre { get; set; }
        public int IdEstadoPedido { get; set; }
        public DateTime? UltimaActualizacion { get; set; }
        public string NumeroPedido { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string RucFacturacion { get; set; }
        public string RazonSocial { get; set; }
        public int IdTipoPedido { get; set; }

        public virtual Ciudad IdCiudadNavigation { get; set; }
        public virtual EstadoPedido IdEstadoPedidoNavigation { get; set; }
        public virtual Fase IdFaseNavigation { get; set; }
        public virtual Oportunidad IdOportunidadNavigation { get; set; }
        public virtual TipoPedido IdTipoPedidoNavigation { get; set; }
    }
}
