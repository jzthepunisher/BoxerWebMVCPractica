using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controllers
{
    public class AsignacionUbicacionGrupoController : Controller
    {
        public ActionResult AsignarUbicacionGrupo()
        {
            //if (opcionesConsulta.CampoOrdenamiento == "------")
            //{
            //    opcionesConsulta.CampoOrdenamiento = "descripcion";
            //    opcionesConsulta.EntidadOrdenamiento = "empleado";
            //    opcionesConsulta.BusquedaRapida = true;
            //    opcionesConsulta.TamanoPagina = 12;
            //}

            //var listaTurnoGrupoVistaModelo = turnosGruposvicio.Get(opcionesConsulta);

            //ListaResultado<TurnoGrupoVistaModelo> listaResultadoVistaModelo;
            //listaResultadoVistaModelo = new ListaResultado<TurnoGrupoVistaModelo>(listaTurnoGrupoVistaModelo, opcionesConsulta);

            //return View(listaResultadoVistaModelo);

            return View();

        }
    }
}
