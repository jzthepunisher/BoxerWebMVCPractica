using BoxerWeb.Domain.Entidades;
using BoxerWeb.WebUI.Servicios;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controllers
{
    public class TurnosController : Controller
    {
        private TurnoServicio turnoServicio;

        public TurnosController()
        {
            turnoServicio = new TurnoServicio();

            AutoMapper.Mapper.CreateMap<Turno, TurnoVistaModelo>();
        }
               
        public ActionResult Listado([Form] OpcionesConsulta opcionesConsulta)
        {
            if (opcionesConsulta.CampoOrdenamiento == "------")
            {
                opcionesConsulta.CampoOrdenamiento = "descripcion";
            }

            var listaTurnoVistaModelo = turnoServicio.Get(opcionesConsulta);

            ListaResultado<TurnoVistaModelo> listaResultadoVistaModelo;
            listaResultadoVistaModelo = new ListaResultado<TurnoVistaModelo>(listaTurnoVistaModelo, opcionesConsulta);

            return View(listaResultadoVistaModelo);

        }

        public ActionResult EditarCrear([Form] String idTurno)
        {
            if (idTurno == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var turno = turnoServicio.GetPorId(idTurno);

            if (turno == null)
            {
                return View(new TurnoVistaModelo());
            }
            else
            {
                return View(AutoMapper.Mapper.Map<Turno, TurnoVistaModelo>(turno));
            }
           
        }

        // GET: Turnos/Detalles/01       
        public ActionResult Detalles([Form] String idTurno)
        {
            if (idTurno == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var turno = turnoServicio.GetPorId(idTurno);

            if (turno == null)
            {
                return View(new TurnoVistaModelo());
            }
            else
            {
                return View(AutoMapper.Mapper.Map<Turno, TurnoVistaModelo>(turno));
            }

            //return View(AutoMapper.Mapper.Map<Autor, AutorVistaModelo>(autor));
        }

        [ChildActionOnly]
        public PartialViewResult ListadoRapido()
        {
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                turnoServicio.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}