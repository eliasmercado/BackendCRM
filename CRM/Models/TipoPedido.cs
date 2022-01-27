using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class TipoPedido
    {
        public TipoPedido()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdTipoPedido { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
