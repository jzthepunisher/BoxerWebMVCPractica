using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class GrupoVistaModelo
    {
        [JsonProperty(PropertyName = "idGrupo")]
        public string IdGrupo { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty(PropertyName = "rastreoGps")]
        public bool RastreoGps { get; set; }

        [JsonProperty(PropertyName = "verEnMapa")]
        public bool VerEnMapa { get; set; }

        [JsonProperty(PropertyName = "fechaHoraUltimaUbicacion")]
        public DateTime? FechaHoraUltimaUbicacion { get; set; }

        [JsonProperty(PropertyName = "direccionUltimaUbicacion")]
        public string DireccionUltimaUbicacion { get; set; }

        [JsonProperty(PropertyName = "empleados")]
        public virtual ICollection<EmpleadoVistaModelo> EmpleadosVistaModelo { get; set; }

        [JsonProperty(PropertyName = "unidadesReaccion")]
        public virtual ICollection<UnidadReaccionVistaModelo> UnidadesReaccionVistaModelo { get; set; }

        [JsonProperty(PropertyName = "dispositivos")]
        public virtual ICollection<DispositivoVistaModelo> DispositivosVistaModelo { get; set; }      
    }
}
