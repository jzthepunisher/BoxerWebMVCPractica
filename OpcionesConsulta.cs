using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class OpcionesConsulta
    {
        public OpcionesConsulta()
        {
            PaginaActual = 1;
            TamanoPagina = 10;

            CampoOrdenamiento = "------";
            Orden = VistaModelo.SortOrder.ASC.ToString();
            CadenaFiltro = "";
            BusquedaRapida = false;
            EntidadOrdenamiento = "------";
        }

        [JsonProperty(PropertyName = "paginaActual")]
        public int PaginaActual { get; set; }

        [JsonProperty(PropertyName = "totalPaginas")]
        public int TotalPaginas { get; set; }

        [JsonProperty(PropertyName = "tamanoPagina")]
        public int TamanoPagina { get; set; }

        [JsonProperty(PropertyName = "campoOrdenamiento")]
        public string CampoOrdenamiento { get; set; }

        [JsonProperty(PropertyName = "orden")]
        public string Orden { get; set; }

        [JsonProperty(PropertyName = "cadenaFiltro")]
        public string CadenaFiltro { get; set; }
        
        [JsonProperty(PropertyName = "busquedaRapida")]
        public Boolean BusquedaRapida { get; set; }

        [JsonProperty(PropertyName = "entidadOrdenamiento")]
        public string EntidadOrdenamiento { get; set; }

        [JsonProperty(PropertyName = "campoFiltro01")]
        public string CampoFiltro01 { get; set; }

        [JsonProperty(PropertyName = "campoValorFiltro01")]
        public string CampoValorFiltro01 { get; set; }

        [JsonIgnore]
        public string Ordena
        {
            get
            {
                return string.Format("{0} {1}",
                    CampoOrdenamiento, Orden);
            }
        }
    }
}
