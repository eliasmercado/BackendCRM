using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Comunicacions = new HashSet<Comunicacion>();
            Contactos = new HashSet<Contacto>();
            Empresas = new HashSet<Empresa>();
            Oportunidads = new HashSet<Oportunidad>();
            Tareas = new HashSet<Tarea>();
            Tickets = new HashSet<Ticket>();
        }

        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdPerfil { get; set; }
        public bool? Estado { get; set; }

        public virtual Perfil IdPerfilNavigation { get; set; }
        public virtual ICollection<Comunicacion> Comunicacions { get; set; }
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
        public virtual ICollection<Oportunidad> Oportunidads { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
