using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class DispositivoEditarCrearVistaModelo
    {    
        [JsonProperty(PropertyName = "imei")]
        public string Imei { get; set; }

        [JsonProperty(PropertyName = "idSimCard")]
        public string IdSimCard { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty(PropertyName = "numeroCelular")]
        public string NumeroCelular { get; set; }

        [JsonProperty(PropertyName = "enviado")]
        public Boolean Enviado { get; set; }

        [JsonProperty(PropertyName = "recibido")]
        public Boolean Recibido { get; set; }

        [JsonProperty(PropertyName = "validado")]
        public Boolean Validado { get; set; }

        [JsonProperty(PropertyName = "estadoSincronizacion")]
        public string EstadoSincronizacion { get; set; }

        [JsonProperty(PropertyName = "fechaCreacion")]
        public DateTime FechaCreacion { get; set; }
    }
}
