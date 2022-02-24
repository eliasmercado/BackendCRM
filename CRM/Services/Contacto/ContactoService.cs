using CRM.Helpers;
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
            Contacto contacto = _context.Contactos.Find(id);

            return contacto;
        }

        public string ModificarContactoById(int id, Contacto contactoModificado)
        {
            if (id != contactoModificado.IdContacto)
            {
                throw new ApiException("Identificador de Contacto no válido");
            }

            _context.Entry(contactoModificado).State = EntityState.Modified;

            try
            {
                 _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(id))
                {
                    throw new ApiException("El contacto no existe");
                }
                else
                {
                    throw;
                }
            }

            return "Contacto modificado";
        }

        public string CrearContacto(Contacto contactoNuevo)
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
