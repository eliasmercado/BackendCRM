using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Seguridad
{
    public class MenuPermisoDTO
    {
        public int IdMenu { get; set; }
        public int IdMenuPadre { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool TienePermiso { get; set; }
    }

    public class DatoPermisoDTO
    {
        public int IdPerfil { get; set; }
        public int IdMenu { get; set; }
        public bool ActivarMenu { get; set; }
    }

}
