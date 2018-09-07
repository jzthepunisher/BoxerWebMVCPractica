using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class UnidadReaccionEditarCrearVistaModelo
    {
        [JsonProperty(PropertyName = "idUnidadReaccion")]
        public string IdUnidadReaccion { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty(PropertyName = "idTipoUnidadReaccion")]
        public string IdTipoUnidadReaccion { get; set; }

        [JsonProperty(PropertyName = "tipoUnidadReaccion")]
        public virtual TipoUnidadReaccionVistaModelo TipoUnidadReaccion { get; set; }

        [JsonProperty(PropertyName = "placa")]
        public string Placa { get; set; }

        [JsonProperty(PropertyName = "idMarca")]
        public string IdMarca { get; set; }

        [JsonProperty(PropertyName = "marca")]
        public virtual MarcaVistaModelo Marca { get; set; }

        [JsonProperty(PropertyName = "modelo")]
        public string Modelo { get; set; }

        [JsonProperty(PropertyName = "idColor")]
        public string IdColor { get; set; }

        [JsonProperty(PropertyName = "color")]
        public virtual ColorVistaModelo Color { get; set; }
    }
}
