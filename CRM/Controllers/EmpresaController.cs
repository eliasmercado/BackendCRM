using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using CRM.Models;
using CRM.DTOs.Empresa;
using CRM.Helpers;
using CRM.Services.ContactoService;

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
        public ApiResponse<List<EmpresaDTO>> GetEmpresas()
        {
            try
            {
                ApiResponse<List<EmpresaDTO>> response = new();

                response.Data = EmpresaService.ObtenerListaEmpresas();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Contacto/5
        [HttpGet("{id}")]
        public ApiResponse<EmpresaDTO> GetEmpresa(int id)
        {
            try
            {
                ApiResponse<EmpresaDTO> response = new();

                var empresa = EmpresaService.ObtenerEmpresaById(id);
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
                response.Data = EmpresaService.ModificarEmpresaById(id, empresa);
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
