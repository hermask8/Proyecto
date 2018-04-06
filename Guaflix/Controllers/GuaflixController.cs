using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Guaflix.Models;
using Guaflix.TamañoFijo;

namespace Guaflix.Controllers
{
    public class GuaflixController : Controller
    {
        ArbolesB.ArbolBusqueda<int, Peliculas> miArbol;
        // GET: Guaflix
        public ActionResult Index()
        {
            miArbol = new ArbolesB.ArbolB<Peliculas>(5,"TreeB",new FabricarTexto());
            return View();
        }

        public ActionResult ValidarUsiario()
        {
            return View();
        }

        public ActionResult CrearUsuario()
        {
            return View();
        }

    }
}