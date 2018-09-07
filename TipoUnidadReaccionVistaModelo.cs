using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class TipoUnidadReaccionVistaModelo
    {
        [JsonProperty(PropertyName = "idTipoUnidadReaccion")]
        public string IdTipoUnidadReaccion { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }
             
        [JsonProperty(PropertyName = "foto")]
        public string Foto { get; set; }
    }
}
