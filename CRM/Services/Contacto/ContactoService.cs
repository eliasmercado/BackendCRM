using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.ContactoService
{
    public class ContactoService
    {
        private CrmDbContext _context;

        public ContactoService(CrmDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un contacto dado su Id
        /// </summary>
        /// <returns></returns>
        public List<Contacto> ObtenerListaContactos()
        {
            List<Contacto> listaContacto = _context.Contactos.Where(x => x.Estado).ToList();

            return listaContacto;
        }

        public Contacto ObtenerContactoById(int id)
        {
            Contacto contacto = _context.Contactos.Single(x => x.IdContacto == id);

            return contacto;
        }

        public String ModificarContactoById(int id, Contacto contactoModificado)
        {
            Contacto contacto = _context.Contactos.Single(x => x.IdContacto == id);
            if (contacto != null) {
                _context.Entry(contacto).State = EntityState.Modified;
                contacto = contactoModificado;
            }

            return "Contacto modificado";
        }

        public String CrearContacto(Contacto contactoNuevo)
        {
            _context.Contactos.Add(contactoNuevo);

            return "Contacto agregado";
        }

        public bool ContactoExists(int id) {
            bool existe = false;
            var contacto = _context.Contactos.Single(x => x.IdContacto == id);
            if (contacto != null)
                existe = true;
            return existe;
        }
    }
}
