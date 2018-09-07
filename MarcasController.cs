using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controllers
{
    public class MarcasController : Controller
    {
        [ChildActionOnly]
        public PartialViewResult ListadoRapido()
        {           
            return PartialView();
        }
    }
}
