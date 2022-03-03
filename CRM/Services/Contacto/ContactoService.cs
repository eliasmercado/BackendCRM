using CRM.DTOs.Seguridad;
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
        public List<ContactoDTO> ObtenerListaContactos()
        {
            List<ContactoDTO> listaContacto = (from contacto in _context.Contactos
                                               where contacto.Estado && !contacto.EsLead
                                               select new ContactoDTO()
                                               {
                                                   IdContacto = contacto.IdContacto,
                                                   Nombres = contacto.Nombres,
                                                   Apellidos = contacto.Apellidos,
                                                   Celular = contacto.Celular,
                                                   Email = contacto.Email,
                                                   FechaNacimiento = contacto.FechaNacimiento,
                                                   IdTipoDocumento = contacto.IdTipoDocumento,
                                                   Documento = contacto.Documento,
                                                   IdCiudad = contacto.IdCiudad,
                                                   Direccion = contacto.Direccion,
                                                   IdEstadoCivil = contacto.IdEstadoCivil,
                                                   IdActividadEconomica = contacto.IdActividadEconomica,
                                                   NombreEmpresa = contacto.NombreEmpresa,
                                                   DireccionLaboral = contacto.DireccionLaboral,
                                                   TelefonoLaboral = contacto.TelefonoLaboral,
                                                   CorreoLaboral = contacto.CorreoLaboral,
                                                   IdPropietario = contacto.IdPropietario
                                               }).ToList();

            return listaContacto;
        }

        public ContactoDTO ObtenerContactoById(int id)
        {
            ContactoDTO contacto = (from contact in _context.Contactos
                                    where contact.Estado && !contact.EsLead && contact.IdContacto == id
                                    select new ContactoDTO()
                                    {
                                        IdContacto = contact.IdContacto,
                                        Nombres = contact.Nombres,
                                        Apellidos = contact.Apellidos,
                                        Celular = contact.Celular,
                                        Email = contact.Email,
                                        FechaNacimiento = contact.FechaNacimiento,
                                        IdTipoDocumento = contact.IdTipoDocumento,
                                        Documento = contact.Documento,
                                        IdCiudad = contact.IdCiudad,
                                        Direccion = contact.Direccion,
                                        IdEstadoCivil = contact.IdEstadoCivil,
                                        IdActividadEconomica = contact.IdActividadEconomica,
                                        NombreEmpresa = contact.NombreEmpresa,
                                        DireccionLaboral = contact.DireccionLaboral,
                                        TelefonoLaboral = contact.TelefonoLaboral,
                                        CorreoLaboral = contact.CorreoLaboral,
                                        IdPropietario = contact.IdPropietario
                                    }).FirstOrDefault();

            return contacto;
        }

        public string ModificarContactoById(int id, ContactoDTO contactoModificado)
        {
            if (id != contactoModificado.IdContacto)
            {
                throw new ApiException("Identificador de Contacto no válido");
            }

            Contacto contacto = _context.Contactos.Find(id);

            if (contacto == null)
                throw new ApiException("El contacto no existe.");

            contacto.Nombres = contactoModificado.Nombres;
            contacto.Apellidos = contactoModificado.Apellidos;
            contacto.Celular = contactoModificado.Celular;
            contacto.Email = contactoModificado.Email;
            contacto.FechaNacimiento = contactoModificado.FechaNacimiento;
            contacto.IdTipoDocumento = contactoModificado.IdTipoDocumento;
            contacto.Documento = contactoModificado.Documento;
            contacto.IdCiudad = contactoModificado.IdCiudad;
            contacto.Direccion = contactoModificado.Direccion;
            contacto.IdEstadoCivil = contactoModificado.IdEstadoCivil;
            contacto.IdActividadEconomica = contactoModificado.IdActividadEconomica;
            contacto.NombreEmpresa = contactoModificado.NombreEmpresa;
            contacto.DireccionLaboral = contactoModificado.DireccionLaboral;
            contacto.TelefonoLaboral = contactoModificado.TelefonoLaboral;
            contacto.CorreoLaboral = contactoModificado.CorreoLaboral;
            contacto.IdPropietario = contactoModificado.IdPropietario;
            _context.SaveChanges();

            return "Contacto modificado";
        }

        public string CrearContacto(ContactoDTO contactoNuevo)
        {
            Contacto contacto = new()
            {
                Nombres = contactoNuevo.Nombres,
                Apellidos = contactoNuevo.Apellidos,
                Celular = contactoNuevo.Celular,
                Email = contactoNuevo.Email,
                FechaNacimiento = contactoNuevo.FechaNacimiento,
                IdTipoDocumento = contactoNuevo.IdTipoDocumento,
                Documento = contactoNuevo.Documento,
                IdCiudad = contactoNuevo.IdCiudad,
                Direccion = contactoNuevo.Direccion,
                IdEstadoCivil = contactoNuevo.IdEstadoCivil,
                IdActividadEconomica = contactoNuevo.IdActividadEconomica,
                NombreEmpresa = contactoNuevo.NombreEmpresa,
                DireccionLaboral = contactoNuevo.DireccionLaboral,
                TelefonoLaboral = contactoNuevo.TelefonoLaboral,
                CorreoLaboral = contactoNuevo.CorreoLaboral,
                IdPropietario = contactoNuevo.IdPropietario,
                EsLead = false
            };
            _context.Contactos.Add(contacto);
            _context.SaveChanges();

            return "Contacto agregado";
        }

        public string EliminarContacto(int id)
        {
            Contacto contacto = _context.Contactos.Where(x => x.Estado && x.IdContacto == id).FirstOrDefault();

            if (contacto == null)
                throw new ApiException("El contacto no existe.");

            contacto.Estado = false;

            _context.Entry(contacto).State = EntityState.Modified;
            _context.SaveChanges();

            return "Contacto eliminado";
        }
}
}
