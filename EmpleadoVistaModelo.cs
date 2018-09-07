using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class EmpleadoVistaModelo
    {
        [JsonProperty(PropertyName = "idEmpleado")]
        public string IdEmpleado { get; set; }

        [JsonProperty(PropertyName = "nombres")]
        public string Nombres { get; set; }

        [JsonProperty(PropertyName = "apellidoPaterno")]
        public string ApellidoPaterno { get; set; }

        [JsonProperty(PropertyName = "apellidoMaterno")]
        public string ApellidoMaterno { get; set; }

        [JsonProperty(PropertyName = "dni")]
        public string DNI { get; set; }

        [JsonProperty(PropertyName = "cargo")]
        public string Cargo { get; set; }

        [JsonProperty(PropertyName = "foto")]
        public string Foto { get; set; }

        [JsonProperty(PropertyName = "idGrupo")]
        public string IdGrupo { get; set; }

        [JsonProperty(PropertyName = "grupo")]
        public string Grupo { get; set; }
    }
}
