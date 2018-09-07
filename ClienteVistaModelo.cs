using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class ClienteVistaModelo
    {
        [JsonProperty(PropertyName = "idCliente")]
        public string IdCliente { get; set; }

        [JsonProperty(PropertyName = "primerNombre")]
        public string PrimerNombre { get; set; }

        [JsonProperty(PropertyName = "segundoNombre")]
        public string SegundoNombre { get; set; }

        [JsonProperty(PropertyName = "apellidoPaterno")]
        public string ApellidoPaterno { get; set; }

        [JsonProperty(PropertyName = "apellidoMaterno")]
        public string ApellidoMaterno { get; set; }

        [JsonProperty(PropertyName = "numeroDocumento")]
        public string NumeroDocumento { get; set; }

        [JsonProperty(PropertyName = "direccion")]
        public string Direccion { get; set; }

        [JsonProperty(PropertyName = "razonSocial")]
        public string RazonSocial { get; set; }

        [JsonProperty(PropertyName = "idTipoDocumento")]
        public string IdTipoDocumento { get; set; }
    }
}
