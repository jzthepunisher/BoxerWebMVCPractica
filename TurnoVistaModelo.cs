using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class TurnoVistaModelo
    {
        [JsonProperty(PropertyName = "idTurno")]
        public string IdTurno { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty(PropertyName = "horaInicio")]
        public TimeSpan HoraInicio { get; set; }

        [JsonProperty(PropertyName = "horaFin")]
        public TimeSpan HoraFin { get; set; }

        [JsonProperty(PropertyName = "gruposAsignados")]
        public int GruposAsignados { get; set; }
    }
}
