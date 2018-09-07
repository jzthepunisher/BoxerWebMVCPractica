using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class MarcaVistaModelo
    {
        [JsonProperty(PropertyName = "idMarca")]
        public string IdMarca { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }
       
    }
}
