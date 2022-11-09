using CRM.DTOs.Dashboard;
using CRM.Helpers;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Dashboard
{
    public class DashboardService
    {
        private CrmDbContext _context;

        public DashboardService(CrmDbContext context)
        {
            _context = context;
        }

        public CantidadRegistroDTO ObtenerCantidadRegistros()
        {
            //Obtenemos las tareas
            IQueryable<Tarea> tareas = _context.Tareas.Where(x => x.FechaCierre.Year == DateTime.Now.Year);

            //Obtenemos los contactos y empresas, obtenemos todos y luego dividimos por leads o cliente
            IQueryable<Contacto> contactos = _context.Contactos.Where(x => x.Estado);
            IQueryable<Empresa> empresas = _context.Empresas.Where(x => x.Estado);

            //Obtenemos las oportunidades
            IQueryable<Oportunidad> oportunidades = _context.Oportunidads.Where(x => x.IdEtapaNavigation.Descripcion != Defs.OPORTUNIDAD_CANCELADA);

            CantidadRegistroDTO cantidad = new()
            {
                Tareas = new EstructuraRegistroDTO()
                {
                    Cantidad = tareas.Where(x => x.IdEstadoActividadNavigation.Descripcion == Defs.TAREA_ABIERTA).Count(),
                    Total = tareas.Count()
                },
                Leads = new EstructuraRegistroDTO()
                {
                    Cantidad = contactos.Where(x => x.FechaCreacion.Month == DateTime.Now.Month && x.EsLead).Count()
                                + empresas.Where(x => x.FechaCreacion.Month == DateTime.Now.Month && x.EsLead).Count(),
                    Total = contactos.Where(x => x.EsLead).Count() + empresas.Where(x => x.EsLead).Count()
                },
                Contactos = new EstructuraRegistroDTO()
                {
                    Cantidad = contactos.Where(x => x.FechaCreacion.Month == DateTime.Now.Month && !x.EsLead).Count()
                                + empresas.Where(x => x.FechaCreacion.Month == DateTime.Now.Month && !x.EsLead).Count(),
                    Total = contactos.Where(x => !x.EsLead).Count() + empresas.Where(x => !x.EsLead).Count()
                },
                OportunidadesAbiertas = new EstructuraRegistroDTO()
                {
                    Cantidad = oportunidades.Where(x => x.IdEtapaNavigation.Descripcion != Defs.OPORTUNIDAD_GANADA && x.IdEtapaNavigation.Descripcion != Defs.OPORTUNIDAD_PERDIDA).Count(),
                    Total = oportunidades.Count()
                },
                OportunidadesGanadas = new EstructuraRegistroDTO()
                {
                    Cantidad = oportunidades.Where(x => x.IdEtapaNavigation.Descripcion == Defs.OPORTUNIDAD_GANADA).Count(),
                    Total = oportunidades.Count()
                }
            };

            return cantidad;
        }

        public List<EstructuraVentaDTO> ObtenerVentasPorMes()
        {

            List<EstructuraVentaDTO> ventas = new List<EstructuraVentaDTO>
            {
                new EstructuraVentaDTO()
                {
                    Mes = "Enero",
                    Cantidad = 10
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Febreo",
                    Cantidad = 100
                },
                 new EstructuraVentaDTO()
                {
                    Mes = "Marzo",
                    Cantidad = 80
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Abril",
                    Cantidad = 110
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Mayo",
                    Cantidad = 30
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Junio",
                    Cantidad = 50
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Julio",
                    Cantidad = 40
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Agosto",
                    Cantidad = 60
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Setiembre",
                    Cantidad = 95
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Octubre",
                    Cantidad = 62
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Noviembre",
                    Cantidad = 22
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Diciembre",
                    Cantidad = 44
                }
            };

            return ventas;
        }

        public List<EstructuraFuenteDTO> ObtenerFuentesOportunidad()
        {

            List<EstructuraFuenteDTO> fuentes = new List<EstructuraFuenteDTO>
            {
                new EstructuraFuenteDTO()
                {
                    Fuente = "CRM",
                    Cantidad = 10
                },
                new EstructuraFuenteDTO()
                {
                    Fuente = "Ecommerce",
                    Cantidad = 20
                },
                new EstructuraFuenteDTO()
                {
                    Fuente = "Landing Page",
                    Cantidad = 5
                },
                new EstructuraFuenteDTO()
                {
                    Fuente = "Sitio Web",
                    Cantidad = 17
                },
                new EstructuraFuenteDTO()
                {
                    Fuente = "Facebook",
                    Cantidad = 2
                },
                new EstructuraFuenteDTO()
                {
                    Fuente = "Instagram",
                    Cantidad = 0
                },
            };

            return fuentes;
        }

        public List<EstructuraCategoriaDTO> ObtenerVentasPorCategoria()
        {

            List<EstructuraCategoriaDTO> categorias = new List<EstructuraCategoriaDTO>
            {
                new EstructuraCategoriaDTO()
                {
                   Categoria = "Categoria 1",
                    Cantidad = 100,
                    CategoriaPadre =""
                },
                new EstructuraCategoriaDTO()
                {
                   Categoria = "Categoria 2",
                    Cantidad = 112,
                    CategoriaPadre =""
                },
                new EstructuraCategoriaDTO()
                {
                   Categoria = "Categoria 3",
                    Cantidad = 53,
                    CategoriaPadre =""
                },
                new EstructuraCategoriaDTO()
                {
                   Categoria = "Categoria hijo 1",
                    Cantidad = 10,
                    CategoriaPadre ="Categoria 1"
                },
                new EstructuraCategoriaDTO()
                {
                   Categoria = "Categoria hijo 1.2",
                    Cantidad = 80,
                    CategoriaPadre ="Categoria 1"
                },
                new EstructuraCategoriaDTO()
                {
                   Categoria = "Categoria hijo 1.3",
                    Cantidad = 10,
                    CategoriaPadre ="Categoria 1"
                },
                new EstructuraCategoriaDTO()
                {
                   Categoria = "Categoria hijo 2",
                    Cantidad = 112,
                    CategoriaPadre ="Categoria 2"
                },
                new EstructuraCategoriaDTO()
                {
                   Categoria = "Categoria hijo 3",
                    Cantidad = 10,
                    CategoriaPadre ="Categoria 3"
                },
                new EstructuraCategoriaDTO()
                {
                   Categoria = "Categoria hijo 2.3",
                    Cantidad = 30,
                    CategoriaPadre ="Categoria 3"
                },
                new EstructuraCategoriaDTO()
                {
                   Categoria = "Categoria hijo 3.3",
                    Cantidad = 13,
                    CategoriaPadre ="Categoria 3"
                },
            };

            return categorias;
        }
    }
}
