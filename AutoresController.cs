using BoxerWeb.WebUI.Servicios;
using BoxerWeb.WebUI.VistaModelo;

using BoxerWeb.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Net;

namespace BoxerWeb.WebUI.Controllers
{

    [RoutePrefix("Autores")]
    public class AutoresController:Controller
    {
        private AutorServicio autorServicio;

        public AutoresController()
        {
            autorServicio = new AutorServicio();

            AutoMapper.Mapper.CreateMap<Autor, AutorVistaModelo>();
        }

        // GET: Autores  
        [Route("Listado")]
        public ActionResult Listado([Form] OpcionesConsulta opcionesConsulta)
        {
            if (opcionesConsulta.CampoOrdenamiento == "------")
            {
                opcionesConsulta.CampoOrdenamiento = "apellido";
            }

            var autores = autorServicio.Get(opcionesConsulta);

            var listaAutorVistaModelo = AutoMapper.Mapper.Map<List<Autor>, List<AutorVistaModelo>>(autores.ToList());

            ListaResultado<AutorVistaModelo> listaResultadoVistaModelo;
            listaResultadoVistaModelo = new ListaResultado<AutorVistaModelo>(listaAutorVistaModelo, opcionesConsulta);

            return View(listaResultadoVistaModelo);           
        }

        // GET: Autores/Crear
        //[BasicAuthorization]
        public ActionResult Crear()
        {
            return View("Form", new AutorVistaModelo());
        }

        // GET: Authors/Detalles/5
        [Route("Detalles/{id:int:min(0)?}")]
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var autor = autorServicio.GetPorId(id.Value);

            if (autor == null)
            {
                return View(new AutorVistaModelo());
            }
            else
            {
                return View(AutoMapper.Mapper.Map<Autor, AutorVistaModelo>(autor));
            }           

            //return View(AutoMapper.Mapper.Map<Autor, AutorVistaModelo>(autor));
        }

        // GET: Authors/Details/5
        [Route("Editar/{id:int:min(0)?}")]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var autor = autorServicio.GetPorId(id.Value);

            if (autor == null)
            {
                return View(new AutorVistaModelo());
            }
            else
            {
                return View(AutoMapper.Mapper.Map<Autor, AutorVistaModelo>(autor));
            }

            //return View(AutoMapper.Mapper.Map<Autor, AutorVistaModelo>(autor));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                autorServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
