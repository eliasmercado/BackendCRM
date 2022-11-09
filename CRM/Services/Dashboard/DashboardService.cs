using CRM.DTOs.Dashboard;
using CRM.Helpers;
using CRM.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.Dashboard
{
    public class DashboardService
    {
        private CrmDbContext _context;
        public IConfiguration Configuration { get; }

        public DashboardService(CrmDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
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
            IQueryable<Oportunidad> oportunidades = _context.Oportunidads.Where(x => x.IdEtapaNavigation.Descripcion == Defs.OPORTUNIDAD_GANADA
                                        && x.FechaCreacion.Year == DateTime.Now.Year);

            List<EstructuraVentaDTO> ventas = new List<EstructuraVentaDTO>
            {
                new EstructuraVentaDTO()
                {
                    Mes = "Enero",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 1).Count()
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Febreo",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 2).Count()
                },
                 new EstructuraVentaDTO()
                {
                    Mes = "Marzo",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 3).Count()
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Abril",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 4).Count()
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Mayo",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 5).Count()
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Junio",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 6).Count()
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Julio",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 7).Count()
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Agosto",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 8).Count()
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Setiembre",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 9).Count()
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Octubre",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 10).Count()
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Noviembre",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 11).Count()
                },
                new EstructuraVentaDTO()
                {
                    Mes = "Diciembre",
                    Cantidad = oportunidades.Where(x => x.FechaCreacion.Month == 12).Count()
                }
            };

            return ventas;
        }

        public List<EstructuraFuenteDTO> ObtenerFuentesOportunidad()
        {
            List<EstructuraFuenteDTO> fuentes = (from oportunidad in _context.Oportunidads
                                                 where oportunidad.FechaCreacion.Year == DateTime.Now.Year
                                                 group oportunidad by oportunidad.IdFuenteNavigation.Descripcion into fuente
                                                 select new EstructuraFuenteDTO
                                                 {
                                                     Fuente = fuente.Key,
                                                     Cantidad = fuente.Count()
                                                 }).ToList();


            return fuentes;
        }

        public List<EstructuraCategoriaDTO> ObtenerVentasPorCategoria()
        {
            string sql = @"
                            select
	                            COUNT(1) as Cantidad,
	                            c2.Nombre as Categoria,
	                            '' AS CategoriaPadre
                            from
	                            DetalleOportunidad do
                            join Producto p on
	                            do.IdProducto = p.IdProducto
                            join Categoria c on
	                            c.IdCategoria = p.IdCategoria
                            join Categoria c2 on
	                            c2.IdCategoria = c.IdCategoriaPadre
                            GROUP by
	                            c2.Nombre
                            UNION
                            select
	                            COUNT(1) as Cantidad,
	                            c.Nombre as Categoria,
		                            c2.Nombre as CategoriaPadre
                            from
	                            DetalleOportunidad do
                            join Producto p on
	                            do.IdProducto = p.IdProducto
                            join Categoria c on
	                            c.IdCategoria = p.IdCategoria
                            join Categoria c2 on
	                            c2.IdCategoria = c.IdCategoriaPadre
                            GROUP by
	                            c2.Nombre,
	                            c.Nombre";
            List<EstructuraCategoriaDTO> categorias = new List<EstructuraCategoriaDTO>();

            using (SqlConnection conexion = new SqlConnection(Configuration.GetConnectionString("CrmDbContext")))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(sql, conexion);
                SqlDataReader registro = comando.ExecuteReader();

                while (registro.Read())
                {
                    categorias.Add(new EstructuraCategoriaDTO
                    {
                        Cantidad = Convert.ToInt32(registro["Cantidad"]),
                        Categoria = registro["Categoria"].ToString(),
                        CategoriaPadre = registro["CategoriaPadre"].ToString()

                    });
                };
                conexion.Close();
            }

            return categorias;
        }
    }
}
