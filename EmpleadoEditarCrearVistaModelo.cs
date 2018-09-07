using BoxerWeb.Domain.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class EmpleadoEditarCrearVistaModelo
    {
        [JsonProperty(PropertyName = "idEmpleado")]
        public string IdEmpleado { get; set; }

        [JsonProperty(PropertyName = "nombres")]
        public string Nombres { get; set; }

        [JsonProperty(PropertyName = "apellidoPaterno")]
        public string ApellidoPaterno { get; set; }

        [JsonProperty(PropertyName = "apellidoMaterno")]
        public string ApellidoMaterno { get; set; }

        [JsonProperty(PropertyName = "direccion")]
        public string Direccion { get; set; }

        [JsonProperty(PropertyName = "dni")]
        public string DNI { get; set; }

        [JsonProperty(PropertyName = "fechaNacimiento")]
        public DateTime? FechaNacimiento { get; set; }
        
        [JsonProperty(PropertyName = "celular")]
        public string Celular { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "idCargo")]
        public string IdCargo { get; set; }

        [JsonProperty(PropertyName = "cargo")]
        public CargoVistaModelo Cargo { get; set; }

        [JsonProperty(PropertyName = "fechaIngreso")]
        public DateTime? FechaIngreso { get; set; }

        [JsonProperty(PropertyName = "fechaBaja")]
        public DateTime? FechaBaja { get; set; }

        [JsonProperty(PropertyName = "foto")]
        public string Foto { get; set; }

        [JsonProperty(PropertyName = "idGrupo")]
        public string IdGrupo { get; set; }
    }
}
