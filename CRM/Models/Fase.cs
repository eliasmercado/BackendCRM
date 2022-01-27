using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Fase
    {
        public Fase()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdFase { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
