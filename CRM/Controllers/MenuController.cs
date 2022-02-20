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
    public class MenuController : ControllerBase
    {
        private readonly MenuService MenuService;

        public MenuController(CrmDbContext context)
        {
            MenuService = new MenuService(context);
        }

        [HttpGet]
        public ApiResponse<List<MenuDTO>> GetMenu()
        {
            try
            {
                ApiResponse<List<MenuDTO>> response = new();

                response.Data = MenuService.ObtenerMenu();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{idUsuario}")]
        public ApiResponse<List<MenuDTO>> GetMenu(int idUsuario)
        {
            try
            {
                ApiResponse<List<MenuDTO>> response = new();

                response.Data = MenuService.ObtenerMenuUsuario(idUsuario);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
