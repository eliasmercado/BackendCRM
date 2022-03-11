using CRM.DTOs.Contacto;
using CRM.Helpers;
using CRM.Models;
using System;

namespace CRM.Services.ComunicacionService
{
    public class ComunicacionService
    {
        private CrmDbContext _context;

        public ComunicacionService(CrmDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra una llamada en la base de datos.
        /// </summary>
        /// <param name="llamada"></param>
        /// <returns></returns>
        public string RegistrarLlamada(ComunicacionDTO llamada)
        {
            DateTime fechaComunicacion = llamada.FechaComunicacion;

            Comunicacion comunicacion = new()
            {
                IdEmpresa = llamada.IdEmpresa,
                IdContacto = llamada.IdContacto,
                MotivoComunicacion = llamada.MotivoComunicacion,
                Observacion = llamada.Observacion,
                IdMedioComunicacion = Defs.LLAMADA,
                IdUsuario = llamada.IdUsuario,
                Referencia = llamada.Referencia,
                FechaComunicacion = fechaComunicacion
            };
            _context.Comunicacions.Add(comunicacion);

            ActualizarUltimoContacto(llamada.IdContacto, llamada.IdEmpresa, fechaComunicacion);

            _context.SaveChanges();

            return "La llamada se registró correctamente.";
        }

        /// <summary>
        /// Envia el email al contacto y registra en la base de datos.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string RegistrarEmail(ComunicacionDTO email)
        {
            DateTime fechaComunicacion = DateTime.Now;
            Contacto contacto;
            Empresa empresa;
            string nombre;

            //Obtenemos el contacto o empresa de acuerdo al tipo
            if (email.IdEmpresa == null)
            {
                contacto = _context.Contactos.Find(email.IdContacto);
                contacto.UltimoContacto = fechaComunicacion;
                nombre = contacto.Nombres + " " + contacto.Apellidos;
            }
            else
            {
                empresa = _context.Empresas.Find(email.IdEmpresa);
                empresa.UltimoContacto = fechaComunicacion;
                nombre = empresa.Nombre;
            }

            EmailUtil.EnviarEmail(nombre, email.Referencia, email.MotivoComunicacion, email.ContenidoEmail);

            Comunicacion comunicacion = new()
            {
                IdEmpresa = email.IdEmpresa,
                IdContacto = email.IdContacto,
                MotivoComunicacion = email.MotivoComunicacion,
                Observacion = email.Observacion,
                IdMedioComunicacion = Defs.CORREO,
                IdUsuario = email.IdUsuario,
                Referencia = email.Referencia,
                FechaComunicacion = fechaComunicacion
            };
            _context.Comunicacions.Add(comunicacion);

            _context.SaveChanges();

            return "El correo se envió correctamente.";
        }

        private void ActualizarUltimoContacto(int? idContacto, int? idEmpresa, DateTime ultimoContacto)
        {
            Contacto contacto;
            Empresa empresa;
            if (idEmpresa == null) { 
                contacto = _context.Contactos.Find(idContacto);
                contacto.UltimoContacto = ultimoContacto;
            }
            else { 
                empresa = _context.Empresas.Find(idEmpresa);
                empresa.UltimoContacto = ultimoContacto;
            }
        }
    }
}
