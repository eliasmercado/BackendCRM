using CRM.DTOs.Actividad;
using CRM.DTOs.Empresa;
using CRM.DTOs.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Oportunidad
{
    public class ListaOportunidadDTO
    {
        public int IdOportunidad { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCierre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Etapa { get; set; }
        public string Prioridad { get; set; }
        public string Valor { get; set; }
        public string TipoCliente { get; set; }
        public string Contacto { get; set; }
        public string Propietario { get; set; }
    }

    public class OportunidadInfoDTO : ListaOportunidadDTO
    {
        public string Fuente { get; set; }
        public string Sucursal { get; set; }
        public string Observacion { get; set; }
        public new string FechaCierre { get; set; }
        public string TipoContacto { get; set; }
        public List<DetalleOportunidadDTO> Detalles { get; set; }
        public ContactoInfoDTO ContactoAsociado { get; set; }
        public EmpresaInfoDTO EmpresaAsociada { get; set; }
    }

    public class OportunidadDTO
    {
        public int IdOportunidad { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCierre { get; set; }
        public int IdEtapa { get; set; }
        public int IdPrioridad { get; set; }
        public decimal Valor { get; set; }
        public string TipoCliente { get; set; }
        public string IdContactoAsociado { get; set; }
        public List<DetalleOportunidadDTO> Detalles { get; set; }
        public int IdFuente { get; set; }
        public string Observacion { get; set; }
        public int IdSucursal { get; set; }
        public int IdPropietario { get; set; }
    }

    public class DetalleOportunidadDTO
    {
        public int IdDetalleOportunidad { get; set; }
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
    }

    public class ContactoAsociadoDTO : Contacto.ContactoDTO { }

    public class EmpresaAsociadaDTO : Empresa.EmpresaDTO { }

    public class InfoContacto
    {
        public object DatosContacto { get; set; }
        public string TipoContacto { get; set; }
    }

    public class DetalleContactoDTO
    {
        public string IdContacto { get; set; }
        public string Nombre { get; set; }
    }

    public class EtapaDTO
    {
        public int IdEtapa { get; set; }
        public string Etapa { get; set; }
    }

    public class SelectSucursalDTO
    {
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
    }

    public class FuenteDTO
    {
        public int IdFuente { get; set; }
        public string Fuente { get; set; }
    }
}
