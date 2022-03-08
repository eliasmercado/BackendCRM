using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Contacto
{
    public class ComunicacionDTO
    {
        public int IdComunicacion { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdContacto { get; set; }
        public string MotivoComunicacion { get; set; }
        public string Observacion { get; set; }
        public int IdUsuario { get; set; }
        public string Referencia { get; set; }
    }
}
