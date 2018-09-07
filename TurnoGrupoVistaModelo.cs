using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class TurnoGrupoVistaModelo
    {
        [JsonProperty(PropertyName = "idTurno")]
        public string IdTurno { get; set; }          

        [JsonProperty(PropertyName = "idGrupo")]
        public string IdGrupo { get; set; }

        [JsonProperty(PropertyName = "grupo")]
        public virtual GrupoVistaModelo GrupoVistaModelo { get; set; }

        [JsonProperty(PropertyName = "direccionUbicacionAsignada")]
        public string DireccionUbicacionAsignada { get; set; }

        [JsonProperty(PropertyName = "latitud")]
        public Double? Latitud { get; set; }

        [JsonProperty(PropertyName = "longitud")]
        public Double? Longitud { get; set; }
    }
}
