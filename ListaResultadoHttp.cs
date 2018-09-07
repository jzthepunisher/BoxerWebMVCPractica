using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerWeb.WebUI.VistaModelo
{
    public class ListaResultadoHttp<T>
    {

        public ListaResultadoHttp(T resultado)
        {
            Resultado = resultado;
        }
        public ListaResultadoHttp(List<T> resultados )
        {
            Resultados = resultados;          
        }

        [JsonProperty(PropertyName = "resultados")]
        public List<T> Resultados { get; private set; }

        [JsonProperty(PropertyName = "resultado")]
        public T Resultado { get; private set; }
    }
}
