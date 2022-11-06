using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Dashboard
{
    public class CantidadRegistroDTO
    {
        public EstructuraRegistroDTO Tareas { get; set; }
        public EstructuraRegistroDTO Leads { get; set; }
        public EstructuraRegistroDTO Contactos { get; set; }
        public EstructuraRegistroDTO OportunidadesAbiertas { get; set; }
        public EstructuraRegistroDTO OportunidadesGanadas { get; set; }
    }

    public class EstructuraRegistroDTO
    {
        public int Cantidad { get; set; }
        public int Total { get; set; }
    }

    public class EstructuraVentaDTO
    {
        public string Mes { get; set; }
        public int Cantidad { get; set; }
    }

    public class EstructuraFuenteDTO
    {
        public string Fuente { get; set; }
        public int Cantidad { get; set; }
    }

    public class EstructuraCategoriaDTO
    {
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public string CategoriaPadre { get; set; }
    }
}
