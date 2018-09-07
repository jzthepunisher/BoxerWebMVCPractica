using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controllers
{
    public class TurnosGruposController : Controller
    {
        //private TurnosGruposvicio turnosGruposvicio;

        public TurnosGruposController()
        {
            //turnosGruposvicio = new TurnosGruposvicio();
        }

        public ActionResult AsignarTurnoGrupo([Form] OpcionesConsulta opcionesConsulta)
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

        //////protected override void Dispose(bool disposing)
        //////{
        //////    if (disposing)
        //////    {
        //////        turnosGruposvicio.Dispose();
        //////    }
        //////    base.Dispose(disposing);
        //////}
    }
}
