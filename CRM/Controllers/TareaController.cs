using CRM.DTOs.Actividad;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.Actividad;
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
    public class TareaController : ControllerBase
    {
        private readonly TareaService TareaService;

        public TareaController(CrmDbContext context)
        {
            TareaService = new TareaService(context);
        }

        // GET: api/Tarea
        [HttpGet]
        public ApiResponse<List<ListaTareaDTO>> GetTareas()
        {
            try
            {
                ApiResponse<List<ListaTareaDTO>> response = new();

                response.Data = TareaService.ObtenerListaTareas();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("calendario")]
        public ApiResponse<List<ListaTareaCalendarioDTO>> GetTareasParaCalendario()
        {
            try
            {
                ApiResponse<List<ListaTareaCalendarioDTO>> response = new();

                response.Data = TareaService.ObtenerListaTareasParaCalendario();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{idTarea}")]
        public ApiResponse<TareaDTO> GetTarea(int idTarea)
        {
            try
            {
                ApiResponse<TareaDTO> response = new();

                response.Data = TareaService.ObtenerTareaById(idTarea);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("tipo")]
        public ApiResponse<List<TipoTareaDTO>> GetTipoTarea()
        {
            try
            {
                ApiResponse<List<TipoTareaDTO>> response = new();

                response.Data = TareaService.ObtenerTiposTarea();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("estado")]
        public ApiResponse<List<EstadoTareaDTO>> GetEstadoTarea()
        {
            try
            {
                ApiResponse<List<EstadoTareaDTO>> response = new();

                response.Data = TareaService.ObtenerEstadosTarea();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("contacto")]
        public ApiResponse<List<ContactoDTO>> GetContactos()
        {
            try
            {
                ApiResponse<List<ContactoDTO>> response = new();

                response.Data = TareaService.ObtenerListaContactos();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("empresa")]
        public ApiResponse<List<EmpresaDTO>> GetEmpresas()
        {
            try
            {
                ApiResponse<List<EmpresaDTO>> response = new();

                response.Data = TareaService.ObtenerListaEmpresas();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("oportunidad")]
        public ApiResponse<List<OportunidadDTO>> GetOportunidades()
        {
            try
            {
                ApiResponse<List<OportunidadDTO>> response = new();

                response.Data = TareaService.ObtenerListaOportunidades();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
