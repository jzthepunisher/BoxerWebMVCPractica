using BoxerWeb.Domain.Abstracto;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controllers
{
    public class ProductController : Controller
    {
       
        private ITurnoRepositorio repository;
        public ProductController(ITurnoRepositorio turnoRepositorio)
        {
            this.repository = turnoRepositorio;
        }
        // GET: Horario
        public ViewResult List()
        {
            return View(repository.Products);
        }
    }
}