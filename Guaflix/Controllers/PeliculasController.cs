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
        
        public static List<Usuarios2> users = new List<Usuarios2>();
        public static List<Peliculas2> Pelic = new List<Peliculas2>();
        public static List<Peliculas2> peliculasUsuarios = new List<Peliculas2>();
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
        public ArbolB<Peliculas> watch1 = new ArbolesB.ArbolB<Peliculas>(3, "WatchUser1.Tree", new FabricarTexto());
        public ArbolB<Peliculas> watch2 = new ArbolesB.ArbolB<Peliculas>(3, "WatchUser2.Tree", new FabricarTexto());
        public ArbolB<Peliculas> watch3= new ArbolesB.ArbolB<Peliculas>(3, "WatchUser3.Tree", new FabricarTexto());
        public ArbolB<Peliculas> watch4 = new ArbolesB.ArbolB<Peliculas>(3, "WatchUser4.Tree", new FabricarTexto());
        public ArbolB<Peliculas> watch5 = new ArbolesB.ArbolB<Peliculas>(3, "WatchUseradmin.Tree", new FabricarTexto());
        public GuaflixController guaflix2 = new GuaflixController();
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
            watch1.Cerrar();
            watch2.Cerrar();
            watch3.Cerrar();
            watch4.Cerrar();
            watch5.Cerrar();
            Todo.Cerrar();
            guaflix2.UsersTree.Cerrar();
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
            try
            {
                var modelo = new Peliculas2
                {
                    tipo = pelicula["Tipo"],
                    nombre = pelicula["Nombre"],
                    genero = pelicula["Genero"],
                    lanzamiento = pelicula["AñoLanzamiento"]
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
            catch (Exception)
            {
                cerrarArchivos();
                return View();
            }
               
        }

        public ActionResult CargaDePeliculas()
        {
            cerrarArchivos();
            return View();
        }
        [HttpPost]
        public ActionResult CargaDePeliculas(HttpPostedFileBase archivo)
        {
            try
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
                        i++;
                    }


                }
                cerrarArchivos();
                return View();
            }
            catch (Exception)
            {
                cerrarArchivos();
                return View();
            }
            
        }

        public ActionResult ListadoPeliculas()
        {
            try
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
            catch (Exception)
            {
                cerrarArchivos();
                return View();
            }
           
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
            try
            {
                encontrado.Clear();
                foreach (var model in Pelic)
                {
                    if (nombre != null)
                    {
                        if (nombre == model.nombre)
                        {
                            cerrarArchivos();
                            encontrado.Add(model);
                        }
                    }

                    if (añoLanzamiento != null)
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
            catch (Exception)
            {
                cerrarArchivos();
                return View();
            }
           
        }

        public void usersList()
        {
            
            users.Clear();
            List<string> miLista = new List<string>();
            miLista = guaflix2.UsersTree.miLIstado();
            foreach (var item in miLista)
            {
                var list = item.Split('=');
                var models = new Usuarios2
                {
                    username = list[0].Trim('%'),
                    nombre = list[1].Trim('%'),
                    apellido = list[2].Trim('%'),
                    edad = list[3].Trim('%'),
                    password = list[4].Trim('%'),
                    confirmapassword = list[5].Trim('%')

                };

                users.Add(models);
            }
            guaflix2.UsersTree.Cerrar();
           
        }

        public ActionResult listaEncontrados()
        {
            return View();
        }

        public ActionResult AgregarWatch(string id)
        {
            try
            {
                string nombres = id;
                string us = guaflix2.retorno();
                usersList();
                if (users.Count() != 0)
                {
                    //cuando hay solo un usuario
                    if (users.Count == 1)
                    {
                        if (users.ElementAt(0).username == us)
                        {
                            foreach (var item2 in Pelic)
                            {
                                if (item2.nombre == nombres)
                                {
                                    var model3 = new Peliculas
                                    {
                                        Tipo = item2.tipo,
                                        Nombre = item2.nombre,
                                        AñoLanzamiento = item2.lanzamiento,
                                        Genero = item2.genero
                                    };
                                    watch1.Agregar(model3.Nombre, model3);
                                }
                            }

                        }
                    }

                    if (users.Count() == 2)
                    {
                        if (users.ElementAt(0).username == us)
                        {
                            foreach (var item2 in Pelic)
                            {
                                if (item2.nombre == nombres)
                                {
                                    var model3 = new Peliculas
                                    {
                                        Tipo = item2.tipo,
                                        Nombre = item2.nombre,
                                        AñoLanzamiento = item2.lanzamiento,
                                        Genero = item2.genero
                                    };
                                    watch1.Agregar(model3.Nombre, model3);
                                }
                            }

                        }

                        if (users.ElementAt(1).username == us)
                        {
                            foreach (var item2 in Pelic)
                            {
                                if (item2.nombre == nombres)
                                {
                                    var model3 = new Peliculas
                                    {
                                        Tipo = item2.tipo,
                                        Nombre = item2.nombre,
                                        AñoLanzamiento = item2.lanzamiento,
                                        Genero = item2.genero
                                    };
                                    watch2.Agregar(model3.Nombre, model3);
                                }
                            }

                        }
                    }

                    if (users.Count() == 3)
                    {
                        if (users.ElementAt(0).username == us)
                        {
                            foreach (var item2 in Pelic)
                            {
                                if (item2.nombre == nombres)
                                {
                                    var model3 = new Peliculas
                                    {
                                        Tipo = item2.tipo,
                                        Nombre = item2.nombre,
                                        AñoLanzamiento = item2.lanzamiento,
                                        Genero = item2.genero
                                    };
                                    watch1.Agregar(model3.Nombre, model3);
                                }
                            }

                        }

                        if (users.ElementAt(1).username == us)
                        {
                            foreach (var item2 in Pelic)
                            {
                                if (item2.nombre == nombres)
                                {
                                    var model3 = new Peliculas
                                    {
                                        Tipo = item2.tipo,
                                        Nombre = item2.nombre,
                                        AñoLanzamiento = item2.lanzamiento,
                                        Genero = item2.genero
                                    };
                                    watch2.Agregar(model3.Nombre, model3);
                                }
                            }

                        }
                        if (users.ElementAt(2).username == us)
                        {
                            foreach (var item2 in Pelic)
                            {
                                if (item2.nombre == nombres)
                                {
                                    var model3 = new Peliculas
                                    {
                                        Tipo = item2.tipo,
                                        Nombre = item2.nombre,
                                        AñoLanzamiento = item2.lanzamiento,
                                        Genero = item2.genero
                                    };
                                    watch3.Agregar(model3.Nombre, model3);
                                }
                            }

                        }
                    }
                    if (users.Count == 4)
                    {
                        if (users.ElementAt(0).username == us)
                        {
                            foreach (var item2 in Pelic)
                            {
                                if (item2.nombre == nombres)
                                {
                                    var model3 = new Peliculas
                                    {
                                        Tipo = item2.tipo,
                                        Nombre = item2.nombre,
                                        AñoLanzamiento = item2.lanzamiento,
                                        Genero = item2.genero
                                    };
                                    watch1.Agregar(model3.Nombre, model3);
                                }
                            }

                        }

                        if (users.ElementAt(1).username == us)
                        {
                            foreach (var item2 in Pelic)
                            {
                                if (item2.nombre == nombres)
                                {
                                    var model3 = new Peliculas
                                    {
                                        Tipo = item2.tipo,
                                        Nombre = item2.nombre,
                                        AñoLanzamiento = item2.lanzamiento,
                                        Genero = item2.genero
                                    };
                                    watch2.Agregar(model3.Nombre, model3);
                                }
                            }

                        }
                        if (users.ElementAt(2).username == us)
                        {
                            foreach (var item2 in Pelic)
                            {
                                if (item2.nombre == nombres)
                                {
                                    var model3 = new Peliculas
                                    {
                                        Tipo = item2.tipo,
                                        Nombre = item2.nombre,
                                        AñoLanzamiento = item2.lanzamiento,
                                        Genero = item2.genero
                                    };
                                    watch3.Agregar(model3.Nombre, model3);
                                }
                            }

                        }
                        if (users.ElementAt(3).username == us)
                        {
                            foreach (var item2 in Pelic)
                            {
                                if (item2.nombre == nombres)
                                {
                                    var model3 = new Peliculas
                                    {
                                        Tipo = item2.tipo,
                                        Nombre = item2.nombre,
                                        AñoLanzamiento = item2.lanzamiento,
                                        Genero = item2.genero
                                    };
                                    watch4.Agregar(model3.Nombre, model3);
                                }
                            }

                        }
                    }

                }

                if ("admin" == us)
                {
                    foreach (var item2 in Pelic)
                    {
                        if (item2.nombre == nombres)
                        {
                            var model3 = new Peliculas
                            {
                                Tipo = item2.tipo,
                                Nombre = item2.nombre,
                                AñoLanzamiento = item2.lanzamiento,
                                Genero = item2.genero
                            };
                            watch5.Agregar(model3.Nombre, model3);
                        }
                    }

                }
                cerrarArchivos();
                guaflix2.UsersTree.Cerrar();
                return View();
            }
            catch (Exception)
            {
                cerrarArchivos();
                guaflix2.UsersTree.Cerrar();
                return View();
            }
          
            
        }
      

        public ActionResult ListadoPorUsuario()
        {
            try
            {
                peliculasUsuarios.Clear();
                string suersw = guaflix2.retorno();
                usersList();
                guaflix2.UsersTree.Cerrar();
                if (users.Count() == 1)
                {
                    if (users.ElementAt(0).username == suersw)
                    {
                        peliculasUsuarios.Clear();
                        List<string> nuevo = new List<string>();
                        nuevo = watch1.miLIstado();
                        foreach (var item2 in nuevo)
                        {
                            var apartar = item2.Split('=');
                            foreach (var item in apartar)
                            {
                                var pel = new Peliculas2
                                {
                                    tipo = apartar[0].Trim('%'),
                                    nombre = apartar[1].Trim('%'),
                                    lanzamiento = apartar[2].Trim('%'),
                                    genero = apartar[3].Trim('%')

                                };
                                peliculasUsuarios.Add(pel);
                            }
                        }
                        cerrarArchivos();
                        return View(peliculasUsuarios);
                    }
                }

                if (users.Count() == 2)
                {
                    if (users.ElementAt(0).username == suersw)
                    {
                        peliculasUsuarios.Clear();
                        List<string> nuevo = new List<string>();
                        nuevo = watch1.miLIstado();
                        foreach (var item2 in nuevo)
                        {
                            var apartar = item2.Split('=');
                            foreach (var item in apartar)
                            {
                                var pel = new Peliculas2
                                {
                                    tipo = apartar[0].Trim('%'),
                                    nombre = apartar[1].Trim('%'),
                                    lanzamiento = apartar[2].Trim('%'),
                                    genero = apartar[3].Trim('%')

                                };
                                peliculasUsuarios.Add(pel);
                            }
                        }
                        cerrarArchivos();
                        return View(peliculasUsuarios);
                    }

                    if (users.ElementAt(1).username == suersw)
                    {

                        peliculasUsuarios.Clear();
                        List<string> nuevo = new List<string>();
                        nuevo = watch2.miLIstado();
                        foreach (var item2 in nuevo)
                        {
                            var apartar = item2.Split('=');
                            foreach (var item in apartar)
                            {
                                var pel = new Peliculas2
                                {
                                    tipo = apartar[0].Trim('%'),
                                    nombre = apartar[1].Trim('%'),
                                    lanzamiento = apartar[2].Trim('%'),
                                    genero = apartar[3].Trim('%')

                                };
                                peliculasUsuarios.Add(pel);
                            }
                        }
                        cerrarArchivos();
                        return View(peliculasUsuarios);
                    }
                }

                if (users.Count() == 3)
                {
                    if (users.ElementAt(0).username == suersw)
                    {
                        peliculasUsuarios.Clear();
                        List<string> nuevo = new List<string>();
                        nuevo = watch1.miLIstado();
                        foreach (var item2 in nuevo)
                        {
                            var apartar = item2.Split('=');
                            foreach (var item in apartar)
                            {
                                var pel = new Peliculas2
                                {
                                    tipo = apartar[0].Trim('%'),
                                    nombre = apartar[1].Trim('%'),
                                    lanzamiento = apartar[2].Trim('%'),
                                    genero = apartar[3].Trim('%')

                                };
                                peliculasUsuarios.Add(pel);
                            }
                        }
                        cerrarArchivos();
                        return View(peliculasUsuarios);
                    }

                    if (users.ElementAt(1).username == suersw)
                    {

                        peliculasUsuarios.Clear();
                        List<string> nuevo = new List<string>();
                        nuevo = watch2.miLIstado();
                        foreach (var item2 in nuevo)
                        {
                            var apartar = item2.Split('=');
                            foreach (var item in apartar)
                            {
                                var pel = new Peliculas2
                                {
                                    tipo = apartar[0].Trim('%'),
                                    nombre = apartar[1].Trim('%'),
                                    lanzamiento = apartar[2].Trim('%'),
                                    genero = apartar[3].Trim('%')

                                };
                                peliculasUsuarios.Add(pel);
                            }
                        }
                        cerrarArchivos();
                        return View(peliculasUsuarios);
                    }
                    if (users.ElementAt(2).username == suersw)
                    {

                        peliculasUsuarios.Clear();
                        List<string> nuevo = new List<string>();
                        nuevo = watch3.miLIstado();
                        foreach (var item2 in nuevo)
                        {
                            var apartar = item2.Split('=');
                            foreach (var item in apartar)
                            {
                                var pel = new Peliculas2
                                {
                                    tipo = apartar[0].Trim('%'),
                                    nombre = apartar[1].Trim('%'),
                                    lanzamiento = apartar[2].Trim('%'),
                                    genero = apartar[3].Trim('%')

                                };
                                peliculasUsuarios.Add(pel);
                            }
                        }
                        cerrarArchivos();
                        return View(peliculasUsuarios);
                    }
                }
                if (users.Count() == 4)
                {
                    if (users.ElementAt(0).username == suersw)
                    {
                        peliculasUsuarios.Clear();
                        List<string> nuevo = new List<string>();
                        nuevo = watch1.miLIstado();
                        foreach (var item2 in nuevo)
                        {
                            var apartar = item2.Split('=');
                            foreach (var item in apartar)
                            {
                                var pel = new Peliculas2
                                {
                                    tipo = apartar[0].Trim('%'),
                                    nombre = apartar[1].Trim('%'),
                                    lanzamiento = apartar[2].Trim('%'),
                                    genero = apartar[3].Trim('%')

                                };
                                peliculasUsuarios.Add(pel);
                            }
                        }
                        cerrarArchivos();
                        return View(peliculasUsuarios);
                    }

                    if (users.ElementAt(1).username == suersw)
                    {

                        peliculasUsuarios.Clear();
                        List<string> nuevo = new List<string>();
                        nuevo = watch2.miLIstado();
                        foreach (var item2 in nuevo)
                        {
                            var apartar = item2.Split('=');
                            foreach (var item in apartar)
                            {
                                var pel = new Peliculas2
                                {
                                    tipo = apartar[0].Trim('%'),
                                    nombre = apartar[1].Trim('%'),
                                    lanzamiento = apartar[2].Trim('%'),
                                    genero = apartar[3].Trim('%')

                                };
                                peliculasUsuarios.Add(pel);
                            }
                        }
                        cerrarArchivos();
                        return View(peliculasUsuarios);
                    }
                    if (users.ElementAt(2).username == suersw)
                    {

                        peliculasUsuarios.Clear();
                        List<string> nuevo = new List<string>();
                        nuevo = watch3.miLIstado();
                        foreach (var item2 in nuevo)
                        {
                            var apartar = item2.Split('=');
                            foreach (var item in apartar)
                            {
                                var pel = new Peliculas2
                                {

                                    tipo = apartar[0].Trim('%'),
                                    nombre = apartar[1].Trim('%'),
                                    lanzamiento = apartar[2].Trim('%'),
                                    genero = apartar[3].Trim('%')

                                };
                                peliculasUsuarios.Add(pel);
                            }
                        }
                        cerrarArchivos();
                        return View(peliculasUsuarios);
                    }
                    if (users.ElementAt(3).username == suersw)
                    {

                        peliculasUsuarios.Clear();
                        List<string> nuevo = new List<string>();
                        nuevo = watch4.miLIstado();
                        foreach (var item2 in nuevo)
                        {
                            var apartar = item2.Split('=');
                            foreach (var item in apartar)
                            {
                                var pel = new Peliculas2
                                {

                                    tipo = apartar[0].Trim('%'),
                                    nombre = apartar[1].Trim('%'),
                                    lanzamiento = apartar[2].Trim('%'),
                                    genero = apartar[3].Trim('%')

                                };
                                peliculasUsuarios.Add(pel);
                            }
                        }
                        cerrarArchivos();
                        return View(peliculasUsuarios);
                    }
                }

                if ("admin" == suersw)
                {
                    peliculasUsuarios.Clear();
                    List<string> nuevo = new List<string>();
                    nuevo = watch5.miLIstado();
                    foreach (var item2 in nuevo)
                    {
                        var apartar = item2.Split('=');
                        foreach (var item in apartar)
                        {
                            var pel = new Peliculas2
                            {

                                tipo = apartar[0].Trim('%'),
                                nombre = apartar[1].Trim('%'),
                                lanzamiento = apartar[2].Trim('%'),
                                genero = apartar[3].Trim('%')

                            };
                            peliculasUsuarios.Add(pel);
                        }
                    }
                    cerrarArchivos();
                    return View(peliculasUsuarios);
                }
                cerrarArchivos();
                return View();
            }
            catch (Exception)
            {
                cerrarArchivos();
                return View();
            }
            
        }
  
    }
}