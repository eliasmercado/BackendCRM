using CRM.Models;
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
        /// Obtiene la lista de todos los contactos del sistema
        /// </summary>
        /// <returns></returns>
        public List<Contacto> ObtenerListaContactos()
        {
            List<Contacto> listaContacto = _context.Contactos.Where(x => x.Estado).ToList();

            return listaContacto;
        }

    }
}
