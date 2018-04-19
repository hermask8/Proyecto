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
        public static List<Peliculas2> Pelic = new List<Peliculas2>();
        public ArbolB<Peliculas> seriesTree = new ArbolesB.ArbolB<Peliculas>(3, "name1series.showtree", new FabricarTexto());
        public ArbolB<Peliculas> seriesTree2 = new ArbolesB.ArbolB<Peliculas>(3, "year1series.showtree", new FabricarTexto());
        public ArbolB<Peliculas> seriesTree3 = new ArbolesB.ArbolB<Peliculas>(3, "gender1series.showtree", new FabricarTexto());
        public ArbolB<Peliculas> peliculasTree = new ArbolesB.ArbolB<Peliculas>(3, "name2pelicula.movietree", new FabricarTexto());
        public ArbolB<Peliculas> peliculasTree2 = new ArbolesB.ArbolB<Peliculas>(3, "year2pelicula.movietree", new FabricarTexto());
        public ArbolB<Peliculas> peliculasTree3 = new ArbolesB.ArbolB<Peliculas>(3, "gender2pelicula.movietree", new FabricarTexto());
        public ArbolB<Peliculas> documentalTree = new ArbolesB.ArbolB<Peliculas>(3, "name3documental.documentarytree", new FabricarTexto());
        public ArbolB<Peliculas> documental2 = new ArbolesB.ArbolB<Peliculas>(3, "year3documental.documentarytree", new FabricarTexto());
        public ArbolB<Peliculas> documental3 = new ArbolesB.ArbolB<Peliculas>(3, "gender3documental.documentarytree", new FabricarTexto());
        public ArbolB<Peliculas> Todo = new ArbolesB.ArbolB<Peliculas>(3, "Todo.tree", new FabricarTexto());
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
            Todo.Cerrar();
        }
        public ActionResult Index()
        {
            cerrarArchivos();
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
                var modelo = new Peliculas2
                {
                    tipo = pelicula["Tipo"],
                    nombre = pelicula["Nombre"],
                    genero = pelicula["Genero"],
                    lanzamiento =pelicula["AñoLanzamiento"]
                };
            var model2 = new Peliculas
            {
                Tipo = modelo.tipo,
                AñoLanzamiento = modelo.lanzamiento,
                Nombre = modelo.nombre,
                Genero = modelo.genero
            };
            var compara = modelo.tipo.ToLower();
            string Llave = model2.AñoLanzamiento + model2.Nombre;
            string Llave2 = model2.Genero + model2.Nombre;
            Todo.Agregar(Llave, model2);
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
                    Todo.Agregar(Llave,model2);
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
            Pelic.Clear();
            List<string> nuevos = new List<string>();
            Pelic.Clear();
            nuevos = Todo.miLIstado();
            foreach (var item in nuevos)
            {
                var valores = item.Split('=');
                var model = new Peliculas2
                {
                    tipo = valores[0].Trim('%'),
                    nombre = valores[1].Trim('%'),
                    lanzamiento = valores[2].Trim('%'),
                    genero = valores[3].Trim('%')
                };
                Pelic.Add(model);
            }
            cerrarArchivos();
            return View(Pelic);
        }

        public ActionResult EliminarPelicula()
        {
            peliculasTree2.Cerrar();
            return View();
        }
        public static List<Peliculas2> encontrado = new List<Peliculas2>();

        public ActionResult Busqueda()
        {
            cerrarArchivos();
            return View();
        }

      
        [HttpPost]
        public ActionResult Busqueda(string nombre, string añoLanzamiento, string genero)
        {
            encontrado.Clear();
            foreach (var model in Pelic)
            {
                if (nombre!=null)
                {
                    if (nombre == model.nombre)
                    {
                        cerrarArchivos();
                        encontrado.Add(model);
                    }
                }

                if (añoLanzamiento!=null)
                {
                    if (añoLanzamiento == model.lanzamiento)
                    {
                        cerrarArchivos();
                        encontrado.Add(model);
                    }
                }

                if (genero != null)
                {
                     if (genero == model.genero)
                    {
                        cerrarArchivos();
                        encontrado.Add(model);
                    }
                } 
                
                
            }
            cerrarArchivos();
            return View(encontrado);
        }

        public ActionResult listaEncontrados()
        {
            return View();
        }
    }
}