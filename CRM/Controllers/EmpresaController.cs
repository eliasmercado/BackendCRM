using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using CRM.Models;
using CRM.DTOs.Empresa;
using CRM.Helpers;
using CRM.Services.EmpresaService;
using CRM.DTOs.Contacto;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService EmpresaService;

        public EmpresaController(CrmDbContext context)
        {
            EmpresaService = new EmpresaService(context);
        }

        // GET: api/Contacto
        [HttpGet]
        public ApiResponse<List<EmpresaDTO>> GetEmpresas([FromQuery] bool esLead = false)
        {
            try
            {
                ApiResponse<List<EmpresaDTO>> response = new();

                response.Data = EmpresaService.ObtenerListaEmpresas(esLead);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Contacto/5
        [HttpGet("{id}")]
        public ApiResponse<EmpresaDTO> GetEmpresa(int id, [FromQuery] bool esLead = false)
        {
            try
            {
                ApiResponse<EmpresaDTO> response = new();

                var empresa = EmpresaService.ObtenerEmpresaById(id, esLead);
                if (empresa != null)
                    response.Data = empresa;
                else
                    throw new ApiException("No se encontró empresa");

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

        [HttpPut("{id}")]
        public ApiResponse<object> PutEmpresa(int id, EmpresaDTO empresa)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = EmpresaService.ModificarEmpresa(id, empresa);
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
        [Route("comunicacion/{idEmpresa}")]
        public ApiResponse<List<ListaComunicacionDTO>> GetComunicaciones(int idEmpresa)
        {
            try
            {
                ApiResponse<List<ListaComunicacionDTO>> response = new();

                response.Data = EmpresaService.ObtenerComunicacionesContacto(idEmpresa);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ApiResponse<object> PostEmpresa(EmpresaDTO empresa)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = EmpresaService.CrearEmpresa(empresa);
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
        public ApiResponse<object> DeleteEmpresa(int id)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = EmpresaService.EliminarEmpresa(id);
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
