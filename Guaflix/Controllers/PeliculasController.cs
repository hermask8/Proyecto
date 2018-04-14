using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guaflix.Models;
using RetornoPelis = System.IO;
using Newtonsoft.Json;

namespace Guaflix.Controllers
{
    public class PeliculasController : Controller
    {
        ArbolesB.ArbolBusqueda<int, Peliculas> miArbol;
        // GET: Peliculas
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult IngresoPeliculaManual()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IngresoPeliculaManual(FormCollection pelicula)
        {
            try
            {
                var modelo = new Peliculas
                {
                    Tipo = pelicula["Tipo"],
                    Nombre = pelicula["Nombre"],
                    Genero = pelicula["Genero"],
                    AñoLanzamiento =Convert.ToInt16(pelicula["AñoLanzamiento"])
                };

                miArbol.Agregar(modelo.AñoLanzamiento, modelo);
            }
            catch
            {
                
            }
            return View();
        }

        public ActionResult CargaDePeliculas()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CargaDePeliculas(HttpPostedFileBase archivo)
        {
            var path = RetornoPelis.File.ReadAllText(archivo.FileName);
            var deserealizar = JsonConvert.DeserializeObject<Peliculas>(path);
            //miArbol.RecorrerPreOrden();
            miArbol.Agregar(deserealizar.AñoLanzamiento, deserealizar);
            return View(miArbol);
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