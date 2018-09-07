using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class CargoVistaModelo
    {
        [JsonProperty(PropertyName = "idCargo")]
        public string IdCargo { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }      
    }
}
