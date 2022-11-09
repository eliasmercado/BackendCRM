using CRM.DTOs.Contacto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Empresa
{
    public class EmpresaDTO
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Ruc { get; set; }
        public string Email { get; set; }
        public int IdDepartamento { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; }
        public string NombreRepresentante { get; set; }
        public string CelularRepresentante { get; set; }
        public bool EsLead { get; set; }
        public int IdPropietario { get; set; }
        public List<ListaComunicacionDTO> Comunicaciones { get; set; }
    }
}
