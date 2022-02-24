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
                //no se como hacer la excepcion xd

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        // PUT: api/Contacto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ApiResponse<Object> PutContacto(int id, Contacto contacto)
        {

            ApiResponse<Object> response = new();
            try
            {
                response.Data = ContactoService.ModificarContactoById(id, contacto);            
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoService.ContactoExists(id))
                {
                    throw;
                }
                else
                {
                    throw;
                }
            }

            return response;
        }
        
        // POST: api/Contacto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ApiResponse<Object> PostContacto(Contacto contacto)
        {
            ApiResponse<Object> response = new();
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
