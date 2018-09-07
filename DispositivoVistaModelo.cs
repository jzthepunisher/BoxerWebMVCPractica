using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class DispositivoVistaModelo
    {
        [JsonProperty(PropertyName = "imei")]
        public string Imei { get; set; }

        [JsonProperty(PropertyName = "idSimCard")]
        public string IdSimCard { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty(PropertyName = "numeroCelular")]
        public string NumeroCelular { get; set; }
   
        [JsonProperty(PropertyName = "validado")]
        public Boolean Validado { get; set; }

        [JsonProperty(PropertyName = "idGrupo")]
        public string IdGrupo { get; set; }

        [JsonProperty(PropertyName = "grupo")]
        public string Grupo { get; set; }

        [JsonProperty(PropertyName = "fotoNegro")]
        public string FotoNegro { get; set; }
    }
}
