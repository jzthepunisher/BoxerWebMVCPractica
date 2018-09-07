using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controllers
{
    public class TiposUnidadReaccionController : Controller
    {
        [ChildActionOnly]
        public PartialViewResult ListadoRapido()
        {
            ///var cart = _cartService.GetBySessionId(HttpContext.Session.SessionID);
            return PartialView();
        }
    }
}
