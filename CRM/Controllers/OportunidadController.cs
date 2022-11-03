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

        [HttpGet("{idOportunidad}")]
        public ApiResponse<OportunidadDTO> GetOportunidad(int idOportunidad)
        {
            try
            {
                ApiResponse<OportunidadDTO> response = new();

                response.Data = OportunidadService.ObtenerOportunidadById(idOportunidad);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("info/{idOportunidad}")]
        public ApiResponse<OportunidadInfoDTO> GetOportunidadInfo(int idOportunidad)
        {
            try
            {
                ApiResponse<OportunidadInfoDTO> response = new();

                response.Data = OportunidadService.ObtenerOportunidadInfo(idOportunidad);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ApiResponse<object> PostOportunidad(OportunidadDTO oportunidad)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = OportunidadService.CrearOportunidad(oportunidad);
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

        [HttpPut("actualizar-etapa/{idOportunidad}")]
        public ApiResponse<object> ModificarEtapaOportunidad(int idOportunidad, [FromQuery] string etapa)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = OportunidadService.ModificarEtapaOportunidad(idOportunidad, etapa);
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
        public ApiResponse<object> PutOportunidad(int id, OportunidadDTO oportunidad)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = OportunidadService.ModificarOportunidad(id, oportunidad);
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
        [Route("contacto")]
        public ApiResponse<List<DetalleContactoDTO>> GetContactosAsociados([FromQuery] bool esLead = false)
        {
            try
            {
                ApiResponse<List<DetalleContactoDTO>> response = new();

                response.Data = OportunidadService.ObtenerListaContactos(esLead);

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

        [HttpGet]
        [Route("sucursal")]
        public ApiResponse<List<SelectSucursalDTO>> GetSucursales()
        {
            try
            {
                ApiResponse<List<SelectSucursalDTO>> response = new();

                response.Data = OportunidadService.ObtenerSucursales();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("fuente")]
        public ApiResponse<List<FuenteDTO>> GetFuentes()
        {
            try
            {
                ApiResponse<List<FuenteDTO>> response = new();

                response.Data = OportunidadService.ObtenerFuentes();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
