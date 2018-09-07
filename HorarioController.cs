using BoxerWeb.Domain.Abstracto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoxerWeb.WebUI.Controllers
{
    public class HorarioController : Controller
    {
        private ITurnoRepositorio repository;
        public HorarioController(ITurnoRepositorio turnoRepositorio)
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