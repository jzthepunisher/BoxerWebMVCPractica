using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controllers
{
    public class BaseDatosController : Controller
    {
        // GET: BaseDatos
        public ActionResult Todos()
        {
            return View();
        }

        // GET: BaseDatos
        public ActionResult TodosMaterialize()
        {
            return View();
        }
    }
}