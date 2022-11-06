using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using CRM.Services.Seguridad;
using CRM.Helpers;
using CRM.DTOs.Seguridad;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService UsuarioService;

        public UsuarioController(CrmDbContext context)
        {
            UsuarioService = new UsuarioService(context);
        }

        // GET: api/Usuario
        [HttpGet]
        public ApiResponse<List<UsuarioDTO>> GetUsuarios()
        {
            try
            {
                ApiResponse<List<UsuarioDTO>> response = new();

                response.Data = UsuarioService.ObtenerListaUsuarios();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{idUsuario}")]
        public ApiResponse<object> PutUsuario(int idUsuario, UsuarioDTO usuario)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = UsuarioService.ModificarUsuario(idUsuario, usuario);
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        [HttpPost]
        public ApiResponse<object> PostUsuario(UsuarioDTO usuario)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = UsuarioService.CrearUsuario(usuario);
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        [HttpPost]
        [Route("validar-credencial")]
        public ApiResponse<bool> PostVerificarCredencial(UsuarioCredencialDTO credencial)
        {
            ApiResponse<bool> response = new();
            try
            {
                response.Data = UsuarioService.ValidarPassword(credencial);
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        [HttpPost]
        [Route("validar-username")]
        public ApiResponse<bool> PostVerificarUsername(UsuarioCredencialDTO credencial)
        {
            ApiResponse<bool> response = new();
            try
            {
                response.Data = UsuarioService.ValidarUsername(credencial);
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        [HttpPost]
        [Route("cambiar-password")]
        public ApiResponse<object> PostCambiarPassword(UsuarioCredencialDTO credencial)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = UsuarioService.CambiarPassword(credencial);
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        [HttpDelete("{idUsuario}")]
        public ApiResponse<object> DeleteContacto(int idUsuario)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = UsuarioService.EliminarUsuario(idUsuario);
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        [HttpGet]
        [Route("perfil")]
        public ApiResponse<List<PerfilDTO>> GetPerfiles()
        {
            try
            {
                ApiResponse<List<PerfilDTO>> response = new();

                response.Data = UsuarioService.ObtenerPerfiles();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
