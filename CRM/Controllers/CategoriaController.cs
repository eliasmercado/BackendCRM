using CRM.DTOs.Producto;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.CategoriaService;
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
    public class CategoriaController : ControllerBase
    {

        private readonly CategoriaService CategoriaService;

        public CategoriaController(CrmDbContext context)
        {
            CategoriaService = new CategoriaService(context);
        }

        [HttpGet]
        public ApiResponse<List<CategoriaDTO>> GetCategorias()
        {
            try
            {
                ApiResponse<List<CategoriaDTO>> response = new();

                response.Data = CategoriaService.ObtenerListaCategorias();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("subCategoria")]
        public ApiResponse<List<CategoriaDTO>> GetSubCategorias()
        {
            try
            {
                ApiResponse<List<CategoriaDTO>> response = new();

                response.Data = CategoriaService.ObtenerListaSubCategorias();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public ApiResponse<CategoriaDTO> GetCategoria(int id)
        {
            try
            {
                ApiResponse<CategoriaDTO> response = new();

                var categoria = CategoriaService.ObtenerCategoriaById(id);
                if (categoria != null)
                    response.Data = categoria;
                else
                    throw new ApiException("No se encontró la categoría");

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

        [HttpGet("subCategoriaByPadre/{id}")]
        public ApiResponse<List<CategoriaDTO>> GetSubCategoriaByIdPadre(int id)
        {
            try
            {
                ApiResponse<List<CategoriaDTO>> response = new();

                var categoria = CategoriaService.ObtenerSubCategoriasByIdPadre(id);
                response.Data = categoria;

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
        public ApiResponse<object> PostCategoria(CategoriaDTO categoria)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = CategoriaService.CrearCategoria(categoria);
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
        public ApiResponse<object> PutCategoria(int id, CategoriaDTO categoria)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = CategoriaService.ModificarCategoria(id, categoria);
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
        public ApiResponse<object> DeleteCategoria(int id)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = CategoriaService.EliminarCategoria(id);
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
