using CRM.DTOs.Contacto;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.ComunicacionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComunicacionController : ControllerBase
    {
        private readonly ComunicacionService ComunicacionService;

        public ComunicacionController(CrmDbContext context)
        {
            ComunicacionService = new ComunicacionService(context);
        }

        [HttpPost]
        [Route("llamada")]
        public ApiResponse<object> PostLlamada(ComunicacionDTO comunicacion)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = ComunicacionService.RegistrarLlamada(comunicacion);
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
        [Route("correo")]
        public ApiResponse<object> PostCorreo(ComunicacionDTO comunicacion)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = ComunicacionService.RegistrarEmail(comunicacion);
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
