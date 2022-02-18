using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdPerfil { get; set; }
        public bool? Estado { get; set; }

        public virtual Perfil IdPerfilNavigation { get; set; }
    }
}
