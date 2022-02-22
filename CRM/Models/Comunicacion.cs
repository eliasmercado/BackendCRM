using System;
using System.Collections.Generic;

#nullable disable

namespace CRM.Models
{
    public partial class Comunicacion
    {
        public int IdComunicacion { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdContacto { get; set; }
        public int IdMotivoComunicacion { get; set; }
        public string Observacion { get; set; }
        public int IdMedioComunicacion { get; set; }
        public int IdUsuario { get; set; }

        public virtual Contacto IdContactoNavigation { get; set; }
        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual MedioComunicacion IdMedioComunicacionNavigation { get; set; }
        public virtual MotivoComunicacion IdMotivoComunicacionNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
