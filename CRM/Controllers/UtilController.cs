using CRM.DTOs.Util;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class UtilController : ControllerBase
    {
        private readonly UtilService UtilService;

        public UtilController(CrmDbContext context)
        {
            UtilService = new UtilService(context);
        }

        [HttpGet]
        [Route("propietario")]
        public ApiResponse<List<PropietarioDTO>> GetPropietarios()
        {
            try
            {
                ApiResponse<List<PropietarioDTO>> response = new();

                response.Data = UtilService.ObtenerUsuariosPropietario();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("departamento")]
        public ApiResponse<List<DepartamentoDTO>> GetDepartamentos()
        {
            try
            {
                ApiResponse<List<DepartamentoDTO>> response = new();

                response.Data = UtilService.ObtenerDepartamentos();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("ciudad")]
        public ApiResponse<List<CiudadDTO>> GetCiudades()
        {
            try
            {
                ApiResponse<List<CiudadDTO>> response = new();

                response.Data = UtilService.ObtenerCiudades();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [Route("prioridad")]
        public ApiResponse<List<PrioridadDTO>> GetPrioridades()
        {
            try
            {
                ApiResponse<List<PrioridadDTO>> response = new();

                response.Data = UtilService.ObtenerPrioridades();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
