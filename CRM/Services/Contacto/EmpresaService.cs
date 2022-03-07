﻿using CRM.DTOs.Empresa;
using CRM.Helpers;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace CRM.Services.ContactoService
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
        public List<EmpresaDTO> ObtenerListaEmpresas()
        {
            List<EmpresaDTO> listaEmpresas = (from empresa in _context.Empresas
                                              where empresa.Estado && !empresa.EsLead
                                              select new EmpresaDTO()
                                              {
                                                  IdEmpresa = empresa.IdEmpresa,
                                                  Nombre = empresa.Nombre,
                                                  Celular = empresa.Celular,
                                                  Telefono = empresa.Telefono,
                                                  Ruc = empresa.Ruc,
                                                  Email = empresa.Email,
                                                  IdCiudad = empresa.IdCiudad,
                                                  Direccion = empresa.Direccion,
                                                  UltimoContacto = empresa.UltimoContacto,
                                                  NombreRepresentante = empresa.NombreRepresentante,
                                                  CelularRepresentante = empresa.CelularRepresentante,
                                                  Estado = empresa.Estado,
                                                  IdPropietario = empresa.IdPropietario
                                              }).ToList();

            return listaEmpresas;
        }

        public EmpresaDTO ObtenerEmpresaById(int id)
        {
            EmpresaDTO contacto = (from empresa in _context.Empresas
                                   where empresa.Estado && !empresa.EsLead && empresa.IdEmpresa == id
                                   select new EmpresaDTO()
                                   {
                                       IdEmpresa = empresa.IdEmpresa,
                                       Nombre = empresa.Nombre,
                                       Celular = empresa.Celular,
                                       Telefono = empresa.Telefono,
                                       Ruc = empresa.Ruc,
                                       Email = empresa.Email,
                                       IdCiudad = empresa.IdCiudad,
                                       Direccion = empresa.Direccion,
                                       UltimoContacto = empresa.UltimoContacto,
                                       NombreRepresentante = empresa.NombreRepresentante,
                                       CelularRepresentante = empresa.CelularRepresentante,
                                       IdPropietario = empresa.IdPropietario
                                   }).FirstOrDefault();

            return contacto;
        }

        public string ModificarEmpresaById(int id, EmpresaDTO empresaModificada)
        {
            if (id != empresaModificada.IdEmpresa)
            {
                throw new ApiException("Identificador de Empresa no válido");
            }

            Empresa empresa = _context.Empresas.Find(id);

            if (empresa == null)
                throw new ApiException("La empresa no existe.");

            empresa.Nombre = empresaModificada.Nombre;
            empresa.Celular = empresaModificada.Celular;
            empresa.Telefono = empresaModificada.Telefono;
            empresa.Ruc = empresaModificada.Ruc;
            empresa.Email = empresaModificada.Email;
            empresa.IdCiudad = empresaModificada.IdCiudad;
            empresa.Direccion = empresaModificada.Direccion;
            empresa.UltimoContacto = empresaModificada.UltimoContacto;
            empresa.NombreRepresentante = empresaModificada.NombreRepresentante;
            empresa.CelularRepresentante = empresaModificada.CelularRepresentante;
            empresa.IdPropietario = empresaModificada.IdPropietario;


            _context.SaveChanges();

            return "Empresa modificada";
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
                UltimoContacto = empresaNueva.UltimoContacto,
                NombreRepresentante = empresaNueva.NombreRepresentante,
                CelularRepresentante = empresaNueva.CelularRepresentante,
                IdPropietario = empresaNueva.IdPropietario,
                EsLead = false
            };
            _context.Empresas.Add(empresa);
            _context.SaveChanges();

            return "Empresa agregado";
        }

        public string EliminarEmpresa(int id)
        {
            Empresa empresa = _context.Empresas.Where(x => x.Estado && x.IdEmpresa == id).FirstOrDefault();

            if (empresa == null)
                throw new ApiException("La empresa no existe.");

            empresa.Estado = false;

            _context.Entry(empresa).State = EntityState.Modified;
            _context.SaveChanges();

            return "Empresa eliminada";
        }
    }
}
