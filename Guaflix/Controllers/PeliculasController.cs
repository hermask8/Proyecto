using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guaflix.Models;
using System.IO;
using Newtonsoft.Json;
using Guaflix.TamañoFijo;
using ArbolesB;

namespace Guaflix.Controllers
{
    public class PeliculasController : Controller
    {
        public ArbolB<Peliculas> seriesTree = new ArbolesB.ArbolB<Peliculas>(3, "name1.showtree", new FabricarTexto());
        public ArbolB<Peliculas> seriesTree2 = new ArbolesB.ArbolB<Peliculas>(3, "year1.showtree", new FabricarTexto());
        public ArbolB<Peliculas> seriesTree3 = new ArbolesB.ArbolB<Peliculas>(3, "gender1.showtree", new FabricarTexto());
        public ArbolB<Peliculas> peliculasTree = new ArbolesB.ArbolB<Peliculas>(3, "name2.movietree", new FabricarTexto());
        public ArbolB<Peliculas> peliculasTree2 = new ArbolesB.ArbolB<Peliculas>(3, "year2.movietree", new FabricarTexto());
        public ArbolB<Peliculas> peliculasTree3 = new ArbolesB.ArbolB<Peliculas>(3, "gender2.movietree", new FabricarTexto());
        public ArbolB<Peliculas> documentalTree = new ArbolesB.ArbolB<Peliculas>(3, "name3.documentarytree", new FabricarTexto());
        public ArbolB<Peliculas> documental2 = new ArbolesB.ArbolB<Peliculas>(3, "year3.documentarytree", new FabricarTexto());
        public ArbolB<Peliculas> documental3 = new ArbolesB.ArbolB<Peliculas>(3, "gender3.documentarytree", new FabricarTexto());
        // GET: Peliculas
        public void cerrarArchivos()
        {
            documentalTree.Cerrar();
            documental2.Cerrar();
            documental3.Cerrar();
            peliculasTree.Cerrar();
            peliculasTree2.Cerrar();
            peliculasTree3.Cerrar();
            seriesTree.Cerrar();
            seriesTree2.Cerrar();
            seriesTree3.Cerrar();
        }
        public ActionResult Index()
        {
            cerrarArchivos();
            return View();
        }
        public ActionResult vistaPeliculas()
        {
            cerrarArchivos();
            return View();
        }
        public ActionResult IngresoPeliculaManual()
        {
            cerrarArchivos();
            return View();
        }
        [HttpPost]
       public ActionResult IngresoPeliculaManual(FormCollection pelicula)
        {
                var modelo = new Peliculas
                {
                    Tipo = pelicula["Tipo"].ToLower(),
                    Nombre = pelicula["Nombre"],
                    Genero = pelicula["Genero"],
                    AñoLanzamiento =pelicula["AñoLanzamiento"]
                };
            if (modelo.Tipo=="documental")
            {
                documentalTree.Agregar(modelo.Nombre,modelo);
                documental2.Agregar((modelo.AñoLanzamiento + modelo.Nombre),modelo);
                documental3.Agregar((modelo.Genero + modelo.Nombre),modelo);
            }

            if (modelo.Tipo == "pelicula")
            {
                peliculasTree.Agregar(modelo.Nombre, modelo);
                peliculasTree2.Agregar((modelo.AñoLanzamiento + modelo.Nombre), modelo);
                peliculasTree3.Agregar((modelo.AñoLanzamiento + modelo.Nombre), modelo);
            }

            if (modelo.Tipo == "series")
            {
                seriesTree.Agregar(modelo.Nombre, modelo);
                seriesTree2.Agregar((modelo.AñoLanzamiento + modelo.Nombre), modelo);
                seriesTree3.Agregar((modelo.AñoLanzamiento + modelo.Nombre), modelo);
            }

            cerrarArchivos();
            return View();
        }

        public ActionResult CargaDePeliculas()
        {
            cerrarArchivos();
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
                    var a = "{" + g[i] + "}";
                    var info = JsonConvert.DeserializeObject<Peliculas2>(a);
                    var compara = info.tipo.ToLower();
                    var model2 = new Peliculas()
                    {
                        Nombre = info.nombre,
                        AñoLanzamiento = info.lanzamiento.ToString(),
                        Tipo = info.tipo,
                        Genero = info.genero
                    };

                    string Llave = model2.AñoLanzamiento + model2.Nombre;
                    string Llave2 = model2.Genero + model2.Nombre;
                    if (compara == "documental")
                    {
                        documentalTree.Agregar(model2.Nombre, model2);
                        documental2.Agregar(Llave, model2);
                        documental3.Agregar(Llave2, model2);
                    }

                    if (compara == "pelicula")
                    {
                        peliculasTree.Agregar(model2.Nombre, model2);
                        peliculasTree2.Agregar(Llave, model2);
                        peliculasTree3.Agregar(Llave2, model2);
                    }

                    if (compara == "series")
                    {
                        seriesTree.Agregar(model2.Nombre, model2);
                        seriesTree2.Agregar(Llave, model2);
                        seriesTree3.Agregar(Llave2, model2);
                    }
                    i++;
                }
               
               
            }
            cerrarArchivos();
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