using CRM.DTOs.Empresa;
using CRM.Helpers;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace CRM.Services.EmpresaService
{
    public class EmpresaService
    {
        private CrmDbContext _context;

        public EmpresaService(CrmDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene una Empresa dada su Id
        /// </summary>
        /// <returns></returns>
        public List<EmpresaDTO> ObtenerListaEmpresas(bool esLead)
        {
            List<EmpresaDTO> listaEmpresas = (from empresa in _context.Empresas
                                              where empresa.Estado && empresa.EsLead == esLead
                                              select new EmpresaDTO()
                                              {
                                                  IdEmpresa = empresa.IdEmpresa,
                                                  Nombre = empresa.Nombre,
                                                  Celular = empresa.Celular,
                                                  Telefono = empresa.Telefono,
                                                  Ruc = empresa.Ruc,
                                                  Email = empresa.Email,
                                                  IdDepartamento = _context.Ciudads.Where(x => x.IdCiudad == empresa.IdCiudad).FirstOrDefault().IdDepartamento,
                                                  IdCiudad = empresa.IdCiudad,
                                                  Direccion = empresa.Direccion,
                                                  NombreRepresentante = empresa.NombreRepresentante,
                                                  CelularRepresentante = empresa.CelularRepresentante,
                                                  IdPropietario = empresa.IdPropietario
                                              }).ToList();

            return listaEmpresas;
        }

        public EmpresaDTO ObtenerEmpresaById(int id, bool esLead)
        {
            EmpresaDTO contacto = (from empresa in _context.Empresas
                                   where empresa.Estado && empresa.EsLead == esLead && empresa.IdEmpresa == id
                                   select new EmpresaDTO()
                                   {
                                       IdEmpresa = empresa.IdEmpresa,
                                       Nombre = empresa.Nombre,
                                       Celular = empresa.Celular,
                                       Telefono = empresa.Telefono,
                                       Ruc = empresa.Ruc,
                                       Email = empresa.Email,
                                       IdDepartamento = _context.Ciudads.Where(x => x.IdCiudad == empresa.IdCiudad).FirstOrDefault().IdDepartamento,
                                       IdCiudad = empresa.IdCiudad,
                                       Direccion = empresa.Direccion,
                                       NombreRepresentante = empresa.NombreRepresentante,
                                       CelularRepresentante = empresa.CelularRepresentante,
                                       IdPropietario = empresa.IdPropietario
                                   }).FirstOrDefault();

            return contacto;
        }

        public string ModificarEmpresa(int id, EmpresaDTO empresaModificada)
        {
            if (id != empresaModificada.IdEmpresa)
            {
                throw new ApiException("Identificador de Empresa no válido");
            }

            Empresa empresa = _context.Empresas.Find(id);

            if (empresa == null)
                throw new ApiException("La empresa no existe");

            empresa.Nombre = empresaModificada.Nombre;
            empresa.Celular = empresaModificada.Celular;
            empresa.Telefono = empresaModificada.Telefono;
            empresa.Ruc = empresaModificada.Ruc;
            empresa.Email = empresaModificada.Email;
            empresa.IdCiudad = empresaModificada.IdCiudad;
            empresa.Direccion = empresaModificada.Direccion;
            empresa.NombreRepresentante = empresaModificada.NombreRepresentante;
            empresa.CelularRepresentante = empresaModificada.CelularRepresentante;
            empresa.IdPropietario = empresaModificada.IdPropietario;
            empresa.EsLead = empresaModificada.EsLead;

            _context.SaveChanges();

            return "La empresa modificó correctamente";
        }

        public string CrearEmpresa(EmpresaDTO empresaNueva)
        {
            Empresa empresa = new()
            {
                IdEmpresa = empresaNueva.IdEmpresa,
                Nombre = empresaNueva.Nombre,
                Celular = empresaNueva.Celular,
                Telefono = empresaNueva.Telefono,
                Ruc = empresaNueva.Ruc,
                Email = empresaNueva.Email,
                IdCiudad = empresaNueva.IdCiudad,
                Direccion = empresaNueva.Direccion,
                NombreRepresentante = empresaNueva.NombreRepresentante,
                CelularRepresentante = empresaNueva.CelularRepresentante,
                IdPropietario = empresaNueva.IdPropietario,
                EsLead = empresaNueva.EsLead
            };
            _context.Empresas.Add(empresa);
            _context.SaveChanges();

            return "La empresa se agregó correctamente";
        }

        public string EliminarEmpresa(int id)
        {
            Empresa empresa = _context.Empresas.Where(x => x.Estado && x.IdEmpresa == id).FirstOrDefault();

            if (empresa == null)
                throw new ApiException("La empresa no existe");

            empresa.Estado = false;

            _context.Entry(empresa).State = EntityState.Modified;
            _context.SaveChanges();

            return "La empresa se eliminó correctamente";
        }
    }
}
