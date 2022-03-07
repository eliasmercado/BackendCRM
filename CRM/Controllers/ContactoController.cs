using System;
using CRM.Helpers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using CRM.Services.ContactoService;
using CRM.DTOs.Seguridad;
using CRM.DTOs.Contacto;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactoController : ControllerBase
    {
        private readonly ContactoService ContactoService;

        public ContactoController(CrmDbContext context)
        {
            ContactoService = new ContactoService(context);
        }

        // GET: api/Contacto
        [HttpGet]
        public ApiResponse<List<ContactoDTO>> GetContactos()
        {
            try
            {
                ApiResponse<List<ContactoDTO>> response = new();

                response.Data = ContactoService.ObtenerListaContactos();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/Contacto/5
        [HttpGet("{id}")]
        public ApiResponse<ContactoDTO> GetContacto(int id)
        {
            try
            {
                ApiResponse<ContactoDTO> response = new();

                var contacto = ContactoService.ObtenerContactoById(id);
                if (contacto != null)
                    response.Data = contacto;
                else
                    throw new ApiException("No se encontró el contacto");

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
        public ApiResponse<object> PutContacto(int id, ContactoDTO contacto)
        {
            ApiResponse<object> response = new();

            try
            {
                response.Data = ContactoService.ModificarContactoById(id, contacto);
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
        public ApiResponse<object> PostContacto(ContactoDTO contacto)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = ContactoService.CrearContacto(contacto);
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
        public ApiResponse<object> DeleteContacto(int id)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = ContactoService.EliminarContacto(id);
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
        [Route("tipoDocumento")]
        public ApiResponse<List<TipoDocumentoDTO>> GetTipoDocumentos()
        {
            try
            {
                ApiResponse<List<TipoDocumentoDTO>> response = new();

                response.Data = ContactoService.ObtenerTipoDocumento();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("estadoCivil")]
        public ApiResponse<List<EstadoCivilDTO>> GetEstadoCiviles()
        {
            try
            {
                ApiResponse<List<EstadoCivilDTO>> response = new();

                response.Data = ContactoService.ObtenerEstadoCivil();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("actividadEconomica")]
        public ApiResponse<List<ActividadEconomicaDTO>> GetActividadesEconomicas()
        {
            try
            {
                ApiResponse<List<ActividadEconomicaDTO>> response = new();

                response.Data = ContactoService.ObtenerActividadEconomica();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
