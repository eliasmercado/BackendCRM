using CRM.DTOs.Producto;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.ProductoService;
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
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService ProductoService;

        public ProductoController(CrmDbContext context)
        {
            ProductoService = new ProductoService(context);
        }

        [HttpGet]
        public ApiResponse<List<ProductoDTO>> GetProductos()
        {
            try
            {
                ApiResponse<List<ProductoDTO>> response = new();

                response.Data = ProductoService.ObtenerListaProductos();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public ApiResponse<ProductoDTO> GetProducto(int id)
        {
            try
            {
                ApiResponse<ProductoDTO> response = new();

                var producto = ProductoService.ObtenerProductoById(id);
                if (producto != null)
                    response.Data = producto;
                else
                    throw new ApiException("No se encontró el producto");

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

        [HttpGet]
        [Route("info/{idProducto}")]
        public ApiResponse<InfoProductoDTO> GetInfoProducto(int idProducto)
        {
            try
            {
                ApiResponse<InfoProductoDTO> response = new();

                response.Data = ProductoService.ObtenerInfoProducto(idProducto);

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

        [HttpGet]
        [Route("moneda")]
        public ApiResponse<List<MonedaDTO>> GetMonedas()
        {
            try
            {
                ApiResponse<List<MonedaDTO>> response = new();

                var moneda = ProductoService.ObtenerListaMonedas();
                response.Data = moneda;

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
        public ApiResponse<object> PostProducto(ProductoDTO producto)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = ProductoService.CrearProducto(producto);
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
        public ApiResponse<object> PutProducto(int id, ProductoDTO producto)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = ProductoService.ModificarProducto(id, producto);
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
        public ApiResponse<object> DeleteProducto(int id)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = ProductoService.EliminarProducto(id);
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
