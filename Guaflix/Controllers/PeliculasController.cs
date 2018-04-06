using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Guaflix.Controllers
{
    public class PeliculasController : Controller
    {
        // GET: Peliculas
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult IngresoPeliculaManual()
        {
            return View();
        }

        public ActionResult CargaDePeliculas()
        {
            return View();
        }

        public ActionResult ListadoPeliculas()
        {
            return View();
        }

        public ActionResult EliminarPelicula()
        {
            return View();
        }
    }
}