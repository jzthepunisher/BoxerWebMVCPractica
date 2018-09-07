using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class ListaResultado<T>
    {
        public ListaResultado(List<T> resultados, OpcionesConsulta opcionesConsulta)
        {
            Resultados = resultados;
            OpcionesConsulta = opcionesConsulta;
        }

        [JsonProperty(PropertyName = "opcionesConsulta")]
        public OpcionesConsulta OpcionesConsulta { get; private set; }

        [JsonProperty(PropertyName = "resultados")]
        public List<T> Resultados { get; private set; }
    }
}