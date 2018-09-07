using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controladores
{
    [RoutePrefix("Menu")]
    public class MenuController : Controller
    {
        // GET: Menu
        [Route("Index")]
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult IndexMaterialize()
        {
            return View("IndexMaterialize");
        }
    }
}