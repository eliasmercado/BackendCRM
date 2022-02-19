using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactoController : ControllerBase
    {
        private readonly CrmDbContext _context;

        public ContactoController(CrmDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetContactos()
        {
            return await _context.Contactos.ToListAsync();
        }

        // GET: api/Contacto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contacto>> GetContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return contacto;
        }

        // PUT: api/Contacto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacto(int id, Contacto contacto)
        {
            if (id != contacto.IdContacto)
            {
                return BadRequest();
            }

            _context.Entry(contacto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contacto>> PostContacto(Contacto contacto)
        {
            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContacto", new { id = contacto.IdContacto }, contacto);
        }

        // DELETE: api/Contacto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }

            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactoExists(int id)
        {
            return _context.Contactos.Any(e => e.IdContacto == id);
        }
    }
}
