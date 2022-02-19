using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Seguridad
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SuccessLoginDTO
    {
        public string Token { get; set; }
    }
}
