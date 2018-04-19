using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guaflix.Models;
using System.IO;
using Newtonsoft.Json;
using Guaflix.TamañoFijo;

namespace Guaflix.Controllers
{
    public class PeliculasController : Controller
    {
        //public ArbolesB.ArbolBusqueda<int, Peliculas> seriesTree = new ArbolesB.ArbolB<Peliculas>(5, "name.showtree", new FabricarTexto());
        //public ArbolesB.ArbolBusqueda<int, Peliculas> seriesTree2 = new ArbolesB.ArbolB<Peliculas>(5, "year.showtree", new FabricarTexto());
        //public ArbolesB.ArbolBusqueda<int, Peliculas> seriesTree3 = new ArbolesB.ArbolB<Peliculas>(5, "gender.showtree", new FabricarTexto());
        //public ArbolesB.ArbolBusqueda<int, Peliculas> peliculasTree = new ArbolesB.ArbolB<Peliculas>(5, "name.movietree", new FabricarTexto());
        public ArbolesB.ArbolBusqueda<string, Peliculas> peliculasTree2 = new ArbolesB.ArbolB<Peliculas>(5, "year.movietree", new FabricarTexto());
        //public ArbolesB.ArbolBusqueda<int, Peliculas> peliculasTree3 = new ArbolesB.ArbolB<Peliculas>(5, "gender.movietree", new FabricarTexto());
        //public ArbolesB.ArbolBusqueda<int, Peliculas> documentalTree = new ArbolesB.ArbolB<Peliculas>(5, "name.documentarytree", new FabricarTexto());
        //public ArbolesB.ArbolBusqueda<int, Peliculas> documental2 = new ArbolesB.ArbolB<Peliculas>(5, "year.documentarytree", new FabricarTexto());
        //public ArbolesB.ArbolBusqueda<int, Peliculas> documental3 = new ArbolesB.ArbolB<Peliculas>(5, "gender.documentarytree", new FabricarTexto());
        // GET: Peliculas

        public ActionResult Index()
        {
            peliculasTree2.Cerrar();
            return View();
        }
        public ActionResult vistaPeliculas()
        {
            peliculasTree2.Cerrar();
            return View();
        }
        public ActionResult IngresoPeliculaManual()
        {
            peliculasTree2.Cerrar();
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
                    AñoLanzamiento =pelicula["AñoLanzamiento"]
                };

                peliculasTree2.Agregar(modelo.AñoLanzamiento, modelo);

            peliculasTree2.Cerrar();
            return View();
        }

        public ActionResult CargaDePeliculas()
        {
            peliculasTree2.Cerrar();
            return View();
        }
        [HttpPost]
        public ActionResult CargaDePeliculas(HttpPostedFileBase archivo)
        {
            string pathArchivo = string.Empty;
            if (archivo != null)
            {
                archivo.SaveAs(Server.MapPath("~/JSONFiles" + Path.GetFileName(archivo.FileName)));
                StreamReader sr = new StreamReader(Server.MapPath("~/JSONFiles" + Path.GetFileName(archivo.FileName)));
                var informacion = sr.ReadToEnd();
                string[] g;
                char[] separador = { '{', '}' };
                g = informacion.Split(separador, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 1; i < g.Length; i += 1)
                {
                    string a = "{" + g[i] + "}";
                    var info = JsonConvert.DeserializeObject<Peliculas>(a);
                    peliculasTree2.Agregar(info.AñoLanzamiento, info);
                    i++;
                }
                peliculasTree2.Cerrar();
               
            }
            return View();
        }

        public ActionResult ListadoPeliculas()
        {
            peliculasTree2.Cerrar();
            return View();
        }

        public ActionResult EliminarPelicula()
        {
            peliculasTree2.Cerrar();
            return View();
        }
        
    }
}