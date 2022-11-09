using CRM.DTOs.Seguridad;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermisoController : ControllerBase
    {
        private readonly MenuPermisoService MenuPermisoService;

        public PermisoController(CrmDbContext context)
        {
            MenuPermisoService = new MenuPermisoService(context);
        }

        [HttpGet("{idPerfil}")]
        public ApiResponse<List<MenuPermisoDTO>> GetMenuPermiso(int idPerfil)
        {
            try
            {
                ApiResponse<List<MenuPermisoDTO>> response = new();

                response.Data = MenuPermisoService.ObtenerMenuPermiso(idPerfil);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ApiResponse<object> PostMenuPermiso(DatoPermisoDTO datoPermiso)
        {
            try
            {
                ApiResponse<object> response = new();

                response.Data = MenuPermisoService.HabilitarMenuPermiso(datoPermiso);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
