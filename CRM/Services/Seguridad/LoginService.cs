using CRM.DTOs.Seguridad;
using CRM.Helpers;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Seguridad
{
    public class LoginService
    {
        private readonly CrmDbContext _context;

        public LoginService(CrmDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene los datos de un usuario si las credenciales son correctas
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SuccessLoginDTO CrearSesion(string usuario, string password)
        {
            Usuario user = _context.Usuarios.Where(x => x.UserName == usuario).FirstOrDefault();

            if (user == null)
                throw new ApiException("El usuario no existe");

            if (user.Password == Util.UtilService.Hash(password))
            {
                Perfil perfil = _context.Perfils.Where(x => x.IdPerfil == user.IdPerfil).FirstOrDefault();

                return new SuccessLoginDTO()
                {
                    IdUsuario = user.IdUsuario,
                    Nombres = user.Nombres,
                    Apellidos = user.Apellidos,
                    Email = user.Email,
                    Perfil = perfil.Descripcion,
                    UserName = user.UserName
                };
            }
            else
                throw new ApiException("La contraseña es incorrecta");
        }
    }
}
