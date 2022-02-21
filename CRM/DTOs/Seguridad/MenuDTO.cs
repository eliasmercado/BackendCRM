using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Seguridad
{
    public class MenuDTO
    {
        public int IdMenu { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string MenuUrl { get; set; }
        public int OrdenAparicion { get; set; }
        public List<MenuDTO> SubMenu { get; set; }
    }
}
