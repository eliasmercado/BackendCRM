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

        public SuccessLoginDTO CrearSesion(string usuario, string password)
        {
            Usuario user = _context.Usuarios.Where(x => x.UserName == usuario).FirstOrDefault();

            if (user == null)
                throw new ApiException("El usuario no existe.");

            if (user.Password == password)
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
                throw new ApiException("Credenciales inválidas.");



        }
    }
}
