using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Oportunidad
{
    public class OportunidadDTO
    {
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
    }
}
