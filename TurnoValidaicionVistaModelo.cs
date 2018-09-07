using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class TurnoValidaicionVistaModelo
    {
        [JsonProperty(PropertyName = "inputTextIdTurno")]
        public string InputTextIdTurno { get; set; }
    }
}
