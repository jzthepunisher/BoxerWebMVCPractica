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
    public class DispositivosController : Controller
    {
        private DispositivoServicio dispositivoServicio;

        public DispositivosController()
        {
            dispositivoServicio = new DispositivoServicio();
        }

        public ActionResult Listado([Form] OpcionesConsulta opcionesConsulta)
        {
            if (opcionesConsulta.CampoOrdenamiento == "------")
            {
                opcionesConsulta.CampoOrdenamiento = "descripcion";
            }

            var listaDispositivoVistaModelo = dispositivoServicio.Get(opcionesConsulta);

            ListaResultado<DispositivoVistaModelo> listaResultadoVistaModelo;
            listaResultadoVistaModelo = new ListaResultado<DispositivoVistaModelo>(listaDispositivoVistaModelo, opcionesConsulta);

            return View(listaResultadoVistaModelo);
           
        }

        public ActionResult EditarCrear([Form] String imei)
        {
            if (imei == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dispositivo = dispositivoServicio.GetPorId(imei);

            if (dispositivo == null)
            {
                return View(new DispositivoEditarCrearVistaModelo());
            }
            else
            {
                return View(dispositivo);
            }
        }

        public ActionResult Detalles([Form] String imei)
        {
            if (imei == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dispositivo = dispositivoServicio.GetPorId(imei);

            if (dispositivo == null)
            {
                return View(new DispositivoEditarCrearVistaModelo());
            }
            else
            {
                return View(dispositivo);
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
                dispositivoServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
