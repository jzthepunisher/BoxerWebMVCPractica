using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class EmpleadoValidacionVistaModelo
    {
        [JsonProperty(PropertyName = "inputTextIdEmpleado")]
        public string InputTextIdEmpleado { get; set; }
    }
}
