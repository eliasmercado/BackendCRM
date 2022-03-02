using CRM.DTOs.Seguridad;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Seguridad
{
    public class MenuService
    {
        private CrmDbContext _context;

        public MenuService(CrmDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todos los menu del sistema
        /// </summary>
        /// <returns></returns>
        public List<MenuDTO> ObtenerMenu()
        {
            List<Menu> menuList = _context.Menus.Where(x => x.Estado).ToList();

            return GenerarMenu(menuList);
        }

        /// <summary>
        /// Obtiene la lista de menu al que tiene permiso el perfil asignado al usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<MenuDTO> ObtenerMenuUsuario(int idUsuario)
        {
            int idPerfil = _context.Usuarios.Where(x => x.IdUsuario == idUsuario).FirstOrDefault().IdPerfil;
            List<Menu> menuList = (from menu in _context.Menus
                                  join perfilPermiso in _context.PerfilPermisos on menu.IdMenu equals perfilPermiso.IdMenu
                                  join perfilUsuario in _context.Perfils on perfilPermiso.IdPerfil equals perfilUsuario.IdPerfil
                                  where perfilUsuario.IdPerfil == idPerfil
                                  select menu).ToList();

            return GenerarMenu(menuList);
        }

        private List<MenuDTO> GenerarMenu(List<Menu> menuList)
        {
            List<MenuDTO> menuListResponse = new();

            foreach (var menuObj in menuList)
            {
                //si es null es un nodo padre
                if (menuObj.IdMenuPadre == null)
                {
                    MenuDTO menu = new()
                    {
                        Text = menuObj.Nombre,
                        Path = menuObj.MenuUrl,
                        Icon = menuObj.Icono,
                        OrdenAparicion = menuObj.OrdenAparicion,
                        Items = GenerarSubMenu(menuList, menuObj)
                    };
                    menuListResponse.Add(menu);
                }
            }
            return menuListResponse.OrderBy(x => x.OrdenAparicion).ToList();
        }

        /// <summary>
        /// Obtiene el sub menu hasta el ultimo nivel del arbol
        /// </summary>
        /// <param name="menuList"></param>
        /// <param name="menuObj"></param>
        /// <returns></returns>
        private static List<MenuDTO> GenerarSubMenu(List<Menu> menuList, Menu menuObj)
        {
            List<MenuDTO> menuListResponse = new();

            foreach (var objeto in menuList)
            {
                //la igualdad indica que objeto es hijo de menuObj
                if (objeto.IdMenuPadre == menuObj.IdMenu)
                {
                    MenuDTO menu = new()
                    {
                        Text = objeto.Nombre,
                        Path = objeto.MenuUrl,
                        Icon = objeto.Icono,
                        OrdenAparicion = objeto.OrdenAparicion,
                        Items = GenerarSubMenu(menuList, objeto)
                    };
                    menuListResponse.Add(menu);
                }
            }
            return menuListResponse.OrderBy(x => x.OrdenAparicion).ToList();
        }
    }
}
