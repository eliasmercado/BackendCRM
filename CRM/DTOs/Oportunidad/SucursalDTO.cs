using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Oportunidad
{
    public class SucursalDTO
    {
        public int IdSucursal { get; set; }
        public string Descripcion { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; }
    }
}
