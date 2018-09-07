using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class ContratoMapaTermicoVistaModelo
    {
        [JsonProperty(PropertyName = "idContrato")]
        public string IdContrato { get; set; }      

        [JsonProperty(PropertyName = "latitud")]
        public Double Latitud { get; set; }

        [JsonProperty(PropertyName = "longitud")]
        public Double Longitud { get; set; }
    }
}
