using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Contacto
    {
        public Contacto()
        {
            Comunicacions = new HashSet<Comunicacion>();
            Oportunidads = new HashSet<Oportunidad>();
            Tareas = new HashSet<Tarea>();
            Tickets = new HashSet<Ticket>();
        }

        public int IdContacto { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Documento { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; }
        public DateTime? UltimoContacto { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdActividadEconomica { get; set; }
        public string NombreEmpresa { get; set; }
        public string DireccionLaboral { get; set; }
        public string TelefonoLaboral { get; set; }
        public string CorreoLaboral { get; set; }
        public bool EsLead { get; set; }
        public bool Estado { get; set; }
        public int IdPropietario { get; set; }

        public virtual ActividadEconomica IdActividadEconomicaNavigation { get; set; }
        public virtual Ciudad IdCiudadNavigation { get; set; }
        public virtual EstadoCivil IdEstadoCivilNavigation { get; set; }
        public virtual Usuario IdPropietarioNavigation { get; set; }
        public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; }
        public virtual ICollection<Comunicacion> Comunicacions { get; set; }
        public virtual ICollection<Oportunidad> Oportunidads { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
