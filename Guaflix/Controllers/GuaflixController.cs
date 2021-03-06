﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RetornoPelis = System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Guaflix.Models;
using Guaflix.TamañoFijo;
using Newtonsoft.Json;
using System.IO;
using pathArchivo2 = System.IO;
using System.Web.Script.Serialization;
using ArbolesB;

namespace Guaflix.Controllers
{
    public class GuaflixController : Controller
    {
        
        public static string user = null;
        public static List<Usuarios2> usuariosLista2 = new List<Usuarios2>();
        public ArbolB<Usuarios> UsersTree = new ArbolB<Usuarios>(3, "users.tree", new FabricarTextoUsuarios());
        // GET: Guaflix
        public string retorno()
        {
           // pelis.cerrarArchivos();
            return user;
        }
        public ActionResult Index()
        {
          //  pelis.cerrarArchivos();
            UsersTree.Cerrar();
            return View();
        }

        public ActionResult ValidarUsuario()
        {
            UsersTree.Cerrar();
          //  pelis.cerrarArchivos();
            return View();
        }

        public ActionResult CargarUsuarios()
        {
            UsersTree.Cerrar();
            //pelis.cerrarArchivos();
            return View();
        }
        [HttpPost]
        public ActionResult CargarUsuarios(HttpPostedFileBase archivo)
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
                        string a = "{" + g[i] + "}";
                        var info = JsonConvert.DeserializeObject<Usuarios2>(a);
                        var model = new Usuarios
                        {
                            username = info.username,
                            nombre = info.nombre,
                            apellido = info.apellido,
                            edad = info.edad,
                            password = info.password,
                            confirmapassword = info.password
                        };
                        UsersTree.Agregar(info.nombre, model);
                        i++;
                    }
                    UsersTree.Cerrar();
                    // pelis.cerrarArchivos();
                }
                UsersTree.Cerrar();
                // pelis.cerrarArchivos();
                return View();
            }
            catch (Exception)
            {
                UsersTree.Cerrar();
                return View();
            }
            
        }
        public ActionResult CrearUsuario()
        {
            UsersTree.Cerrar();
          //  pelis.cerrarArchivos();
            return View();
        }
        [HttpPost]
        public ActionResult CrearUsuario(FormCollection persona)
        {
            try
            { 
                var model = new Usuarios
                {
                    username = persona["username"],
                    nombre = persona["nombre"],
                    apellido = persona["apellido"],
                    edad = persona["edad"],
                    password = persona["password"],
                    confirmapassword = persona["confirmpassword"]
                };
                
                
                UsersTree.Agregar(model.username, model);
                UsersTree.Cerrar();
               // pelis.cerrarArchivos();
            }
            catch
            {
                UsersTree.Cerrar();
               // pelis.cerrarArchivos();
            }
            UsersTree.Cerrar();
            //pelis.cerrarArchivos();
            return View();
        }
        
     
        public ActionResult Catálogo()
        {
            UsersTree.Cerrar();
            //pelis.cerrarArchivos();
            return View();
        }
        public ActionResult Login()
        {
            UsersTree.Cerrar();
            //pelis.cerrarArchivos();
            return View();
        }
        [HttpPost]
        public ActionResult Login(string usuario, string contraseña)
        {
            try
            {
                user = usuario;
                usuariosLista2.Clear();
                List<string> milistado = new List<string>();
                milistado = UsersTree.miLIstado();


                foreach (var model in milistado)
                {
                    var valores = model.Split('=');
                    var item = new Usuarios2
                    {
                        username = valores[0].Trim('%'),
                        nombre = valores[1].Trim('%'),
                        edad = valores[2].Trim('%'),
                        apellido = valores[3].Trim('%'),
                        password = valores[4].Trim('%'),
                        confirmapassword = valores[5].Trim('%'),
                    };
                    usuariosLista2.Add(item);
                    UsersTree.Cerrar();
                    // pelis.cerrarArchivos();
                }
                foreach (var item in usuariosLista2)
                {

                    if (usuario == item.username && contraseña == item.password)
                    {
                        UsersTree.Cerrar();
                        // pelis.cerrarArchivos();
                        return RedirectToAction("CatalogoUsuario", "guaflix");
                    }
                }


                if (usuario == "admin" && contraseña == "admin")
                {
                    UsersTree.Cerrar();
                    //pelis.cerrarArchivos();
                    return RedirectToAction("Catálogo", "guaflix");
                }

                else
                {
                    UsersTree.Cerrar();
                    // pelis.cerrarArchivos();
                    //pelis.cerrarArchivos();
                    return RedirectToAction("Error", "Guaflix");
                }
            }
            catch (Exception)
            {
                UsersTree.Cerrar();
                return View();
            }
            
          //  pelis.cerrarArchivos();
        }
        

        public void obtener()
        {
           // pelis.cerrarArchivos();
        }
        public ActionResult Error()
        {
            UsersTree.Cerrar();
           // pelis.cerrarArchivos();
            return View();
        }

        public ActionResult CatalogoUsuario()
        {
            //  pelis.cerrarArchivos();
            UsersTree.Cerrar();
            return View();
        }
    }
}