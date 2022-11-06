using CRM.DTOs.Dashboard;
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
            CantidadRegistroDTO cantidad = new()
            {
                Tareas = new EstructuraRegistroDTO()
                {
                    Cantidad = 5,
                    Total = 10
                },
                Leads = new EstructuraRegistroDTO()
                {
                    Cantidad = 1,
                    Total = 10
                },
                Contactos = new EstructuraRegistroDTO()
                {
                    Cantidad = 2,
                    Total = 101
                },
                OportunidadesAbiertas = new EstructuraRegistroDTO()
                {
                    Cantidad = 2,
                    Total = 101
                },
                OportunidadesGanadas = new EstructuraRegistroDTO()
                {
                    Cantidad = 51,
                    Total = 100
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
