using CRM.DTOs.Contacto;
using CRM.Helpers;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.ComunicacionService
{
    public class ComunicacionService
    {
        private CrmDbContext _context;

        public ComunicacionService(CrmDbContext context)
        {
            _context = context;
        }

        public string RegistrarLlamada(ComunicacionDTO llamada)
        {
            Comunicacion comunicacion = new()
            {
                IdEmpresa = llamada.IdEmpresa,
                IdContacto = llamada.IdContacto,
                MotivoComunicacion = llamada.MotivoComunicacion,
                Observacion = llamada.Observacion,
                IdMedioComunicacion = Defs.LLAMADA,
                IdUsuario = llamada.IdUsuario,
                Referencia = llamada.Referencia,
                FechaComunicacion = DateTime.Now
            };
            _context.Comunicacions.Add(comunicacion);

            ActualizarUltimoContacto(llamada.IdContacto, llamada.IdEmpresa, comunicacion.FechaComunicacion);

            _context.SaveChanges();

            return "La llamada se registró correctamente.";
        }

        public string RegistrarEmail(ComunicacionDTO llamada)
        {
            Comunicacion comunicacion = new()
            {
                IdEmpresa = llamada.IdEmpresa,
                IdContacto = llamada.IdContacto,
                MotivoComunicacion = llamada.MotivoComunicacion,
                Observacion = llamada.Observacion,
                IdMedioComunicacion = Defs.CORREO,
                IdUsuario = llamada.IdUsuario,
                Referencia = llamada.Referencia,
                FechaComunicacion = DateTime.Now
            };
            _context.Comunicacions.Add(comunicacion);

            ActualizarUltimoContacto(llamada.IdContacto, llamada.IdEmpresa, comunicacion.FechaComunicacion);

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
