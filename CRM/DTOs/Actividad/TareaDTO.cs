using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.DTOs.Actividad
{
    public class TareaDTO
    {
        public int IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int IdTipo { get; set; }
        public int IdEstado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public int IdResponsable { get; set; }
        public string AsociarCon { get; set; }
        public int? IdContactoAsociado { get; set; }
        public int? IdEmpresaAsociada { get; set; }
        public int? IdOportunidadAsociada { get; set; }
    }

    public class TareaInfoDTO
    {
        public int IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string FechaInicio { get; set; }
        public string FechaCierre { get; set; }
        public string Responsable { get; set; }
        public string AsociarCon { get; set; }
        public ContactoInfoDTO ContactoAsociado { get; set; }
        public EmpresaInfoDTO EmpresaAsociada { get; set; }
        public OportunidadInfoDTO OportunidadAsociada { get; set; }
    }

    public class ContactoInfoDTO
    {
        public int IdContacto { get; set; }
        public string NombreCompleto { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
    }

    public class EmpresaInfoDTO
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }

    public class OportunidadInfoDTO
    {
        public int IdOportunidad { get; set; }
        public string Nombre { get; set; }
        public string Etapa { get; set; }
        public string Valor { get; set; }
        public string Contacto { get; set; }
    }

    public class ListaTareaDTO
    {
        public int IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Responsable { get; set; }
    }

    public class ListaTareaCalendarioDTO
    {
        public int IdTarea { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusNumber { get; set; }
        public bool AllDay { get; set; }
    }

    public class TipoTareaDTO
    {
        public int IdTipoTarea { get; set; }
        public string TipoTarea { get; set; }
    }

    public class EstadoTareaDTO
    {
        public int IdEstadoTarea { get; set; }
        public string EstadoTarea { get; set; }
    }

    public class ContactoDTO
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; }
    }

    public class EmpresaDTO
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
    }

    public class OportunidadDTO
    {
        public int IdOportunidad { get; set; }
        public string Nombre { get; set; }
    }
}
