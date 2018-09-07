using BoxerWeb.Domain.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class UnidadReaccionVistaModelo
    {
        [JsonProperty(PropertyName = "idUnidadReaccion")]
        public string IdUnidadReaccion { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }
               
        [JsonProperty(PropertyName = "tipoUnidadReaccion")]
        public string TipoUnidadReaccion { get; set; }

        [JsonProperty(PropertyName = "fotoIconoUnidadReaccion")]
        public string FotoIconoUnidadReaccion { get; set; }

        [JsonProperty(PropertyName = "foto")]
        public string Foto { get; set; }

        [JsonProperty(PropertyName = "fotoNegro")]
        public string FotoNegro { get; set; }

        [JsonProperty(PropertyName = "placa")]
        public string Placa { get; set; }             

        [JsonProperty(PropertyName = "marca")]
        public string Marca { get; set; }

        [JsonProperty(PropertyName = "modelo")]
        public string Modelo { get; set; }

        [JsonProperty(PropertyName = "idGrupo")]
        public string IdGrupo { get; set; }

        [JsonProperty(PropertyName = "grupo")]
        public string Grupo { get; set; }

    }
}
