using CRM.DTOs.Seguridad;
using CRM.Helpers;
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
        private readonly TokenService TokenService;
        private readonly LoginService LoginService;

        public LoginController(CrmDbContext context, IConfiguration configuration)
        {
            TokenService = new TokenService(configuration);
            LoginService = new LoginService(context);
        }


        [HttpPost]
        public ApiResponse<SuccessLoginDTO> Authenticate(LoginDTO login)
        {
            try
            {
                ApiResponse<SuccessLoginDTO> response = new();

                SuccessLoginDTO successLogin = LoginService.CrearSesion(login.UserName, login.Password);
                string token = TokenService.CreateToken(new Usuario() { UserName = successLogin.UserName, Email = successLogin.Email });
                response.Data = successLogin;
                response.Data.Token = token;

                return response;
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
