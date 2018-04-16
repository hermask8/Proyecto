using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guaflix.Models;
using RetornoPelis = System.IO;
using Newtonsoft.Json;
using Guaflix.TamañoFijo;

namespace Guaflix.Controllers
{
    public class PeliculasController : Controller
    {
       public ArbolesB.ArbolBusqueda<int, Peliculas> miArbol = new ArbolesB.ArbolB<Peliculas>(5, "TreeB", new FabricarTexto());
        // GET: Peliculas
        
        public ActionResult Index()
        {
            miArbol.Cerrar();
            return View();
        }
        public ActionResult IngresoPeliculaManual()
        {
            miArbol.Cerrar();
            return View();
        }
        [HttpPost]
        public ActionResult IngresoPeliculaManual(FormCollection pelicula)
        {
                var modelo = new Peliculas
                {
                    Tipo = pelicula["Tipo"],
                    Nombre = pelicula["Nombre"],
                    Genero = pelicula["Genero"],
                    AñoLanzamiento =Convert.ToInt16(pelicula["AñoLanzamiento"])
                };

                miArbol.Agregar(modelo.AñoLanzamiento, modelo);

            miArbol.Cerrar();
            return View();
        }

        public ActionResult CargaDePeliculas()
        {
            miArbol.Cerrar();
            return View();
        }
        [HttpPost]
        public ActionResult CargaDePeliculas(HttpPostedFileBase archivo)
        {
            var path = RetornoPelis.File.ReadAllText(archivo.FileName);
            var deserealizar = JsonConvert.DeserializeObject<Peliculas>(path);
            //miArbol.RecorrerPreOrden();
            miArbol.Agregar(deserealizar.AñoLanzamiento, deserealizar);
            miArbol.Cerrar();
            return View(miArbol);
        }

        public ActionResult ListadoPeliculas()
        {
            miArbol.Cerrar();
            return View();
        }

        public ActionResult EliminarPelicula()
        {
            miArbol.Cerrar();
            return View();
        }
        
    }
}