using System;
using CRM.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using CRM.Services;
using CRM.Services.ContactoService;

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
        public ApiResponse<List<Contacto>> GetContactos()
        {
            try
            {
                ApiResponse<List<Contacto>> response = new();

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
        public ApiResponse<Contacto> GetContacto(int id)
        {
            try
            {
                ApiResponse<Contacto> response = new();

                var contacto = ContactoService.ObtenerContactoById(id);
                if (contacto != null)
                    response.Data = ContactoService.ObtenerContactoById(id);
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
        public ApiResponse<object> PutContacto(int id, Contacto contacto)
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
        public ApiResponse<object> PostContacto(Contacto contacto)
        {
            ApiResponse<object> response = new();
            try
            {
                response.Data = ContactoService.CrearContacto(contacto);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return response;
        }
    }
}
