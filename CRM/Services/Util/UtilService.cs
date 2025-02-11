﻿using CRM.DTOs.Util;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Util
{
    public class UtilService
    {
        private CrmDbContext _context;

        public UtilService(CrmDbContext context)
        {
            _context = context;
        }

        public List<PropietarioDTO> ObtenerUsuariosPropietario()
        {
            List<PropietarioDTO> propietarios = (from usuario in _context.Usuarios
                                                 where usuario.Estado
                                                 select new PropietarioDTO()
                                                 {
                                                     IdPropietario = usuario.IdUsuario,
                                                     Propietario = string.Format("{0} {1}", usuario.Nombres, usuario.Apellidos)
                                                 }).ToList();
            return propietarios;
        }

        public List<DepartamentoDTO> ObtenerDepartamentos()
        {
            List<DepartamentoDTO> departamentos = (from departamento in _context.Departamentos
                                                   select new DepartamentoDTO()
                                                   {
                                                       IdDepartamento = departamento.IdDepartamento,
                                                       Departamento = departamento.Descripcion
                                                   }).ToList();
            return departamentos;
        }

        public List<CiudadDTO> ObtenerCiudades()
        {
            List<CiudadDTO> ciudades = (from ciudad in _context.Ciudads
                                                   select new CiudadDTO()
                                                   {
                                                       IdCiudad = ciudad.IdCiudad,
                                                       Ciudad = ciudad.Descripcion,
                                                       IdDepartamento = ciudad.IdDepartamento
                                                   }).ToList();
            return ciudades;
        }

        public List<PrioridadDTO> ObtenerPrioridades()
        {
            List<PrioridadDTO> prioridades = (from prioridad in _context.Prioridads
                                     select new PrioridadDTO()
                                     {
                                         IdPrioridad = prioridad.IdPrioridad,
                                         Prioridad = prioridad.Descripcion
                                     }).ToList();
            return prioridades;
        }

        public static string Hash(string input)
        {
            using var sha1 = SHA1.Create();
            return Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }
    }
}
