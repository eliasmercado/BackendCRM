using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class PerfilPermiso
    {
        public int IdPerfilPermiso { get; set; }
        public int IdPerfil { get; set; }
        public int IdMenu { get; set; }

        public virtual Menu IdMenuNavigation { get; set; }
        public virtual Perfil IdPerfilNavigation { get; set; }
    }
}
