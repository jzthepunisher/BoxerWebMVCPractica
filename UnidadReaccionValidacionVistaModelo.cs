using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class UnidadReaccionValidacionVistaModelo
    {
        [JsonProperty(PropertyName = "inputTextIdUnidadReaccionValida")]
        public string InputTextIdUnidadReaccionValida { get; set; }
    }
}
