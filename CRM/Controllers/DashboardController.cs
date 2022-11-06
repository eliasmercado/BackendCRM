using CRM.DTOs.Dashboard;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.Dashboard;
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
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService DashboardService;

        public DashboardController(CrmDbContext context)
        {
            DashboardService = new DashboardService(context);
        }


        [HttpGet]
        [Route("cantidad-total")]
        public ApiResponse<CantidadRegistroDTO> GetCantidadRegistros()
        {
            try
            {
                ApiResponse<CantidadRegistroDTO> response = new();

                response.Data = DashboardService.ObtenerCantidadRegistros();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("ventas-mes")]
        public ApiResponse<List<EstructuraVentaDTO>> GetVentasPorMes()
        {
            try
            {
                ApiResponse<List<EstructuraVentaDTO>> response = new();

                response.Data = DashboardService.ObtenerVentasPorMes();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("fuentes-oportunidad")]
        public ApiResponse<List<EstructuraFuenteDTO>> GetFuentesOportunidad()
        {
            try
            {
                ApiResponse<List<EstructuraFuenteDTO>> response = new();

                response.Data = DashboardService.ObtenerFuentesOportunidad();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("categorias-oportunidad")]
        public ApiResponse<List<EstructuraCategoriaDTO>> GetCategoriasOportunidad()
        {
            try
            {
                ApiResponse<List<EstructuraCategoriaDTO>> response = new();

                response.Data = DashboardService.ObtenerVentasPorCategoria();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
