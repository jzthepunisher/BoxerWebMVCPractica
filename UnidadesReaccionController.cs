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
    public class UnidadesReaccionController : Controller
    {
        private UnidadReaccionServicio unidadReaccionServicio;

        public UnidadesReaccionController()
        {
            unidadReaccionServicio = new UnidadReaccionServicio();
        }

        public ActionResult Listado([Form] OpcionesConsulta opcionesConsulta)
        {
            if (opcionesConsulta.CampoOrdenamiento == "------")
            {
                opcionesConsulta.CampoOrdenamiento = "descripcion";
            }

            var listaUnidadReaccionVistaModelo = unidadReaccionServicio.Get(opcionesConsulta);

            ListaResultado<UnidadReaccionVistaModelo> listaResultadoVistaModelo;
            listaResultadoVistaModelo = new ListaResultado<UnidadReaccionVistaModelo>(listaUnidadReaccionVistaModelo, opcionesConsulta);

            return View(listaResultadoVistaModelo);

            //////return View();
        }

        public ActionResult EditarCrear([Form] String idUnidadReaccion)
        {
            if (idUnidadReaccion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unidadReaccion = unidadReaccionServicio.GetPorId(idUnidadReaccion);

            if (unidadReaccion == null)
            {
                return View(new UnidadReaccionEditarCrearVistaModelo());
            }
            else
            {                             
                return View(unidadReaccion);
            }

        }
                   
        public ActionResult Detalles([Form] String idUnidadReaccion)
        {
            if (idUnidadReaccion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unidadReaccion = unidadReaccionServicio.GetPorId(idUnidadReaccion);

            if (unidadReaccion == null)
            {
                return View(new UnidadReaccionEditarCrearVistaModelo());
            }
            else
            {
                return View(unidadReaccion);
            }
        }

        [ChildActionOnly]
        public PartialViewResult ListadoRapido()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult ListadoRapido02()
        {
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unidadReaccionServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
