using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class ContratoVistaModelo
    {
        [JsonProperty(PropertyName = "idContrato")]
        public string IdContrato { get; set; }

        [JsonProperty(PropertyName = "direccion")]
        public string Direccion { get; set; }

        [JsonProperty(PropertyName = "latitud")]
        public Double Latitud { get; set; }

        [JsonProperty(PropertyName = "longitud")]
        public Double Longitud { get; set; }

        [JsonProperty(PropertyName = "monitoreoActivo")]
        public Boolean MonitoreoActivo { get; set; }

        [JsonProperty(PropertyName = "idCliente")]
        public string IdCliente { get; set; }

    }
}
