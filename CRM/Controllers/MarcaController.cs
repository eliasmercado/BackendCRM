using CRM.DTOs.Producto;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.MarcaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MarcaController : ControllerBase
    {

        private readonly MarcaService MarcaService;

        public MarcaController(CrmDbContext context)
        {
            MarcaService = new MarcaService(context);
        }

        [HttpGet]
        public ApiResponse<List<MarcaDTO>> GetMarcas()
        {
            try
            {
                ApiResponse<List<MarcaDTO>> response = new();

                response.Data = MarcaService.ObtenerListaMarcas();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public ApiResponse<MarcaDTO> GetMarca(int id)
        {
            try
            {
                ApiResponse<MarcaDTO> response = new();

                var marca = MarcaService.ObtenerMarcaById(id);
                if (marca != null)
                    response.Data = marca;
                else
                    throw new ApiException("No se encontró la marca");

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
        public ApiResponse<object> PostMarca(MarcaDTO marca)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = MarcaService.CrearMarca(marca);
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
        public ApiResponse<object> PutMarca(int id, MarcaDTO marca)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = MarcaService.ModificarMarca(id, marca);
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
