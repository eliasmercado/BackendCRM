using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Sucursal
{
    public class SucursalDTO
    {
        public int IdSucursal { get; set; }
        public string Descripcion { get; set; }
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public string Direccion { get; set; }
    }
}
