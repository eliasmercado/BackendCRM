using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Seguridad
{
    public class MenuDTO
    {
        public string Text { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public int OrdenAparicion { get; set; }
        public List<MenuDTO> Items { get; set; }
    }
}
