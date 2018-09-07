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
    public class ClientesController : Controller
    {
        private ClienteServicio clienteServicio;

        public ClientesController()
        {
            clienteServicio = new ClienteServicio();
        }

        public ActionResult Listado([Form] OpcionesConsulta opcionesConsulta)
        {
            if (opcionesConsulta.CampoOrdenamiento == "------")
            {
                opcionesConsulta.CampoOrdenamiento = "idCliente";
            }

            var listaClienteVistaModelo = clienteServicio.Get(opcionesConsulta);

            ListaResultado<ClienteVistaModelo> listaResultadoVistaModelo;
            listaResultadoVistaModelo = new ListaResultado<ClienteVistaModelo>(listaClienteVistaModelo, opcionesConsulta);

            return View(listaResultadoVistaModelo);

            //////return View();
        }

        public ActionResult EditarCrear([Form] String idCliente)
        {
            if (idCliente == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cliente = clienteServicio.GetPorId(idCliente);

            if (cliente == null)
            {
                return View(new ClienteEditarCrearVistaModelo());
            }
            else
            {
                return View(cliente);
            }

        }
            
        public ActionResult Detalles([Form] String idCliente)
        {
            if (idCliente == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cliente = clienteServicio.GetPorId(idCliente);

            if (cliente == null)
            {
                return View(new ClienteEditarCrearVistaModelo());
            }
            else
            {
                return View(cliente);
            }

            //return View(AutoMapper.Mapper.Map<Autor, AutorVistaModelo>(autor));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                clienteServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
