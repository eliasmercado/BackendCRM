using CRM.DTOs.Sucursal;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.SucursalService;
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
    public class SucursalController : ControllerBase
    {
        private readonly SucursalService SucursalService;

        public SucursalController(CrmDbContext context)
        {
            SucursalService = new SucursalService(context);
        }

        [HttpGet]
        public ApiResponse<List<SucursalDTO>> GetSucursales()
        {
            try
            {
                ApiResponse<List<SucursalDTO>> response = new();

                response.Data = SucursalService.ObtenerSucursales();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public ApiResponse<SucursalDTO> GetSucursal(int id)
        {
            try
            {
                ApiResponse<SucursalDTO> response = new();

                var sucursal = SucursalService.ObtenerSucursal(id);
                if (sucursal != null)
                    response.Data = sucursal;
                else
                    throw new ApiException("No se encontró la sucursal");

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

        [HttpPost]
        public ApiResponse<object> PostSucursal(SucursalDTO sucursal)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = SucursalService.CrearSucursal(sucursal);
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

        [HttpPut("{id}")]
        public ApiResponse<object> PutSucursal(int id, SucursalDTO marca)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = SucursalService.ModificarSucursal(id, marca);
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

        [HttpDelete("{id}")]
        public ApiResponse<object> DeleteSucursal(int id)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = SucursalService.EliminarSucursal(id);
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
    }
}
