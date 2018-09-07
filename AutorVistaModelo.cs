using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class AutorVistaModelo
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "primerNombre")]
        public string PrimerNombre { get; set; }

        [Required]
        [JsonProperty(PropertyName = "apellido")]
        public string Apellido { get; set; }

        [JsonProperty(PropertyName = "biografia")]
        public string Biografia { get; set; }
    }
}
