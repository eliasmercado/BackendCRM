using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Comunicacions = new HashSet<Comunicacion>();
            Oportunidads = new HashSet<Oportunidad>();
            Tareas = new HashSet<Tarea>();
            Tickets = new HashSet<Ticket>();
        }

        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Ruc { get; set; }
        public string Email { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; }
        public DateTime UltimoContacto { get; set; }
        public string NombreRepresentante { get; set; }
        public string CelularRepresentante { get; set; }
        public bool EsLead { get; set; }
        public bool Estado { get; set; }
        public int IdPropietario { get; set; }

        public virtual Ciudad IdCiudadNavigation { get; set; }
        public virtual Usuario IdPropietarioNavigation { get; set; }
        public virtual ICollection<Comunicacion> Comunicacions { get; set; }
        public virtual ICollection<Oportunidad> Oportunidads { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
