using CRM.DTOs.Seguridad;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Seguridad
{
    public class MenuPermisoService
    {
        private CrmDbContext _context;

        public MenuPermisoService(CrmDbContext context)
        {
            _context = context;
        }

        public List<MenuPermisoDTO> ObtenerMenuPermiso(int idPerfil)
        {
            List<MenuPermisoDTO> listaMenuPermiso = (from menu in _context.Menus where menu.Estado
                                                     select new MenuPermisoDTO
                                                     {
                                                         IdMenu = menu.IdMenu,
                                                         Nombre = menu.Nombre,
                                                         Descripcion = menu.Descripcion,
                                                         IdMenuPadre = menu.IdMenuPadre == null ? 0 : menu.IdMenuPadre.Value,
                                                         TienePermiso = false
                                                     }).ToList();

            List<int> permisos = _context.PerfilPermisos.Where(x => x.IdPerfil == idPerfil).Select(x => x.IdMenu).ToList();

            foreach(var menus in listaMenuPermiso)
            {
                if (permisos.Contains(menus.IdMenu))
                    menus.TienePermiso = true;
            }

            return listaMenuPermiso;
        }

        public string HabilitarMenuPermiso(DatoPermisoDTO datoPermiso)
        {
            PerfilPermiso perfilPermiso = _context.PerfilPermisos.Where(x => x.IdMenu == datoPermiso.IdMenu && x.IdPerfil == datoPermiso.IdPerfil).FirstOrDefault();

            //si no existe hay que activar,en caso de que exista eliminamos
            if (perfilPermiso == null && datoPermiso.ActivarMenu) {
                perfilPermiso = new PerfilPermiso();
                perfilPermiso.IdPerfil = datoPermiso.IdPerfil;
                perfilPermiso.IdMenu = datoPermiso.IdMenu;

                _context.PerfilPermisos.Add(perfilPermiso);
            }
            else if (!datoPermiso.ActivarMenu)
            {
                _context.PerfilPermisos.Remove(perfilPermiso);
            }
            _context.SaveChanges();
            
            return "Permiso modificado correctamente";
        }
    }
}
