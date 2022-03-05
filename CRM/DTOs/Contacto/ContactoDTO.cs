using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Seguridad
{
    public class ContactoDTO
    {
        public int IdContacto { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public int IdCiudad { get; set; }
        public int IdDepartamento { get; set; }
        public string Direccion { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdActividadEconomica { get; set; }
        public string NombreEmpresa { get; set; }
        public string DireccionLaboral { get; set; }
        public string TelefonoLaboral { get; set; }
        public string CorreoLaboral { get; set; }
        public int IdPropietario { get; set; }
    }
}
