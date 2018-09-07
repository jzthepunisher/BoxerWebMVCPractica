using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class CargoValidaVistaModelo
    {
        [JsonProperty(PropertyName = "inputTextIdCargo")]
        public string InputTextIdCargo { get; set; }
    }
}
