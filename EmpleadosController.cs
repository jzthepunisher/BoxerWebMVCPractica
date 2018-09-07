using BoxerWeb.Domain.Entidades;
using BoxerWeb.WebUI.Servicios;
using BoxerWeb.WebUI.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controllers
{
    public class EmpleadosController : Controller
    {
        private EmpleadoServicio empleadoServicio;

        public EmpleadosController()
        {
            empleadoServicio = new EmpleadoServicio();            
        }

        public ActionResult Listado([Form] OpcionesConsulta opcionesConsulta)
        {
            if (opcionesConsulta.CampoOrdenamiento == "------")
            {
                opcionesConsulta.CampoOrdenamiento = "apellidoPaterno";
            }

            var listaEmpleadoVistaModelo = empleadoServicio.Get(opcionesConsulta);

            //var listaEmpleadoVistaModelo = AutoMapper.Mapper.Map<List<Empleado>, List<EmpleadoVistaModelo>>(empleados.ToList());

            ListaResultado<EmpleadoVistaModelo> listaResultadoVistaModelo;
            listaResultadoVistaModelo = new ListaResultado<EmpleadoVistaModelo>(listaEmpleadoVistaModelo, opcionesConsulta);

            return View(listaResultadoVistaModelo);
        }

        public ActionResult EditarCrear([Form] String idEmpleado)
        {
            if (idEmpleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empleado = empleadoServicio.GetPorId(idEmpleado);

            if (empleado == null)
            {
                return View(new EmpleadoEditarCrearVistaModelo());
            }
            else
            {
                //////AutoMapper.Mapper.CreateMap<Empleado, EmpleadoVistaModelo>();
                //////return View(AutoMapper.Mapper.Map<Empleado, EmpleadoVistaModelo>(empleado));               
                return View(empleado);
            }

        }

        // GET: Empleados/Detalles/01       
        public ActionResult Detalles([Form] String idEmpleado)
        {
            if (idEmpleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var empleado = empleadoServicio.GetPorId(idEmpleado);

            if (empleado == null)
            {
                return View(new EmpleadoEditarCrearVistaModelo());
            }
            else
            {
                return View(empleado);
            }

            //return View(AutoMapper.Mapper.Map<Autor, AutorVistaModelo>(autor));
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Imagenes/Empleados"), pic);
                // file is uploaded
                file.SaveAs(path);

                //////// save the image path path to the database or you can send image
                //////// directly to database
                //////// in-case if you want to store byte[] ie. for DB
                //////using (MemoryStream ms = new MemoryStream())
                //////{
                //////    file.InputStream.CopyTo(ms);
                //////    byte[] array = ms.GetBuffer();
                //////}

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Listado", "Empleados");
        }

        public ActionResult FileUpload2([Form] String nombreArchivo)
        {
            string nombreFoto="";
            
            if (Request.Files.Count>0)
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                    String fname = Server.MapPath("~/Imagenes/Empleados/" + nombreArchivo + '.' + fileExt); 
                    //string fname = Server.MapPath("~/Imagenes/Empleados/" + file.FileName);
                    file.SaveAs(fname);
                    nombreFoto = nombreArchivo + '.' + fileExt;
                }              
            }
            //return View();
            return Content(nombreFoto);
            //Content(nombreFoto);
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
    }
}
