using CRM.DTOs.Seguridad;
using CRM.Helpers;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Seguridad
{
    public class UsuarioService
    {
        private CrmDbContext _context;

        public UsuarioService(CrmDbContext context)
        {
            _context = context;
        }

        public List<UsuarioDTO> ObtenerListaUsuarios()
        {
            List<UsuarioDTO> listaUsuarios = (from usuario in _context.Usuarios
                                              where usuario.Estado
                                              select new UsuarioDTO()
                                              {
                                                  IdUsuario = usuario.IdUsuario,
                                                  Nombres = usuario.Nombres,
                                                  Apellidos = usuario.Apellidos,
                                                  Email = usuario.Email,
                                                  Direccion = usuario.Direccion,
                                                  Username = usuario.UserName,
                                                  IdPerfil = usuario.IdPerfil
                                              }).ToList();

            return listaUsuarios;
        }

        public string ModificarUsuario(int idUsuario, UsuarioDTO usuarioModificado)
        {
            if (idUsuario != usuarioModificado.IdUsuario)
            {
                throw new ApiException("Identificador de usuario no válido");
            }

            Usuario usuario = _context.Usuarios.Find(idUsuario);

            if (usuario == null)
                throw new ApiException("El usuario no existe");

            usuario.Nombres = usuarioModificado.Nombres;
            usuario.Apellidos = usuarioModificado.Apellidos;
            usuario.Email = usuarioModificado.Email;
            usuario.Direccion = usuarioModificado.Direccion;
            usuario.UserName = usuarioModificado.Username;
            usuario.IdPerfil = usuarioModificado.IdPerfil;

            _context.SaveChanges();

            return "El usuario se modificó correctamente";
        }

        public string CrearUsuario(UsuarioDTO usuarioNuevo)
        {
            Usuario usuario = new()
            {
                Nombres = usuarioNuevo.Nombres,
                Apellidos = usuarioNuevo.Apellidos,
                Email = usuarioNuevo.Email,
                Direccion = usuarioNuevo.Direccion,
                UserName = usuarioNuevo.Username,
                Password = Util.UtilService.Hash(Defs.DEFAULT_PASS),
                FechaCreacion = DateTime.Now,
                IdPerfil = usuarioNuevo.IdPerfil,
                Estado = true
            };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return "El usuario se agregó correctamente";
        }

        public string EliminarUsuario(int idUsuario)
        {
            Usuario usuario = _context.Usuarios.Where(x => x.Estado && x.IdUsuario == idUsuario).FirstOrDefault();

            if (usuario == null)
                throw new ApiException("El usuario no existe");

            usuario.Estado = false;

            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();

            return "El usuario se eliminó correctamente";
        }

        public List<PerfilDTO> ObtenerPerfiles()
        {
            List<PerfilDTO> perfiles = (from perfil in _context.Perfils
                                        where perfil.Estado
                                        select new PerfilDTO()
                                        {
                                            IdPerfil = perfil.IdPerfil,
                                            Perfil = perfil.Descripcion
                                        }).ToList();

            return perfiles;
        }
    }
}
