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

        public Usuario CrearSesion(string usuario, string password)
        {
            Usuario user = _context.Usuarios.Where(x => x.UserName == usuario).FirstOrDefault();

            if (user == null)
                throw new ApiException("El usuario no existe.");
            
            if (user.Password == password)
                return user;
            else
                throw new ApiException("Credenciales inválidas.");
        }
    }
}
