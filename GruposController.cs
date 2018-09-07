using BoxerWeb.WebUI.Servicios;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controllers
{
    public class GruposController : Controller
    {

        private GrupoServicio grupoServicio;

        public GruposController()
        {
            grupoServicio = new GrupoServicio();
        }

        public ActionResult Listado([Form] OpcionesConsulta opcionesConsulta)
        {
            if (opcionesConsulta.CampoOrdenamiento == "------")
            {
                opcionesConsulta.CampoOrdenamiento = "descripcion";
                opcionesConsulta.EntidadOrdenamiento = "empleado";
                opcionesConsulta.BusquedaRapida = true;
                opcionesConsulta.TamanoPagina = 12;
            }

            var listaGrupoVistaModelo = grupoServicio.Get(opcionesConsulta);

            ListaResultado<GrupoVistaModelo> listaResultadoVistaModelo;
            listaResultadoVistaModelo = new ListaResultado<GrupoVistaModelo>(listaGrupoVistaModelo, opcionesConsulta);

            return View(listaResultadoVistaModelo);

            //////return View();

        }

        public ActionResult EditarCrear([Form] String idGrupo)
        {
            if (idGrupo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = grupoServicio.GetPorId(idGrupo);

            if (grupo == null)
            {
                return View(new GrupoEditarCrearVistaModelo());
            }
            else
            {                           
                return View(grupo);
            }

        }

        public ActionResult Detalles([Form] String idGrupo)
        {
            if (idGrupo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var grupo = grupoServicio.GetPorId(idGrupo);

            if (grupo == null)
            {
                return View(new GrupoEditarCrearVistaModelo());
            }
            else
            {
                return View(grupo);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                grupoServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
