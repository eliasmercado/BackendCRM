using CRM.DTOs.Oportunidad;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.OportunidadService;
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
    public class OportunidadController : ControllerBase
    {
        private readonly OportunidadService OportunidadService;

        public OportunidadController(CrmDbContext context)
        {
            OportunidadService = new OportunidadService(context);
        }

        // GET: api/Oportunidad
        [HttpGet]
        public ApiResponse<List<ListaOportunidadDTO>> GetOportunidades()
        {
            try
            {
                ApiResponse<List<ListaOportunidadDTO>> response = new();

                response.Data = OportunidadService.ObtenerListaOportunidades();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public ApiResponse<OportunidadDTO> GetOportunidad()
        {
            try
            {
                ApiResponse<OportunidadDTO> response = new();

                response.Data = OportunidadService.ObtenerOportunidadById();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("contacto")]
        public ApiResponse<List<DetalleContactoDTO>> GetContactosAsociados()
        {
            try
            {
                ApiResponse<List<DetalleContactoDTO>> response = new();

                response.Data = OportunidadService.ObtenerListaContactos();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("etapa")]
        public ApiResponse<List<EtapaDTO>> GetEtapas()
        {
            try
            {
                ApiResponse<List<EtapaDTO>> response = new();

                response.Data = OportunidadService.ObtenerEtapas();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
