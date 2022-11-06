using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Seguridad
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Username { get; set; }
        public int IdPerfil { get; set; }
    }

    public class PerfilDTO
    {
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
    }

    public class UsuarioCredencialDTO
    {
        public int IdUsuario { get; set; }
        public string Password { get; set; }
    }
}
