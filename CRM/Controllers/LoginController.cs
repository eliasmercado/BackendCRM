using CRM.DTOs.Seguridad;
using CRM.Models;
using CRM.Services.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly CrmDbContext _context;
        private readonly TokenService TokenService;

        public LoginController(CrmDbContext context, IConfiguration configuration)
        {
            TokenService = new TokenService(context, configuration);
        }


        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(LoginDTO login)
        {
            if (login == null)
                return BadRequest(new { mensaje = "Enviar credenciales" });

            bool isCredentialValid = (login.Password == "123456");
            if (isCredentialValid)
            {
                var token = TokenService.CreateToken(new Usuario() { User ="elias", Email="eliasmerc23@gmail.com" });
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
