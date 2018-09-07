using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class GrupoValidacionVistaModelo
    {
        [JsonProperty(PropertyName = "inputTextIdGrupoValida")]
        public string InputTextIdGrupoValida { get; set; }
    }
}
