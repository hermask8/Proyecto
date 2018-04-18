using System;
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



namespace Guaflix.Controllers
{
    public class GuaflixController : Controller
    {
        public ArbolesB.ArbolBusqueda<int, Usuarios> miArbol2 = new ArbolesB.ArbolB<Usuarios>(5, "ArbolBUsuarios", new FabricarTextoUsuarios());

        PeliculasController pelis = new PeliculasController();
        // GET: Guaflix
        public ActionResult Index()
        {
            miArbol2.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }

        public ActionResult ValidarUsuario()
        {
            miArbol2.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }

        public ActionResult CargarUsuarios()
        {
            miArbol2.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }
        [HttpPost]
        public ActionResult CargarUsuarios(HttpPostedFileBase archivo)
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
                for (int i = 1; i < g.Length; i+=1)
                {
                    string a = "{" + g[i] + "}";
                    var info = JsonConvert.DeserializeObject<Usuarios>(a);
                    miArbol2.Agregar(info.Edad, info);
                    i++;
                }
                miArbol2.Cerrar();
                pelis.peliculasTree2.Cerrar();
            }
            return View();
        }
        public ActionResult CrearUsuario()
        {
            miArbol2.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }
        [HttpPost]
        public ActionResult CrearUsuario(FormCollection persona)
        {
            try
            { 
                var model = new Usuarios
                {
                    Nombre = persona["Nombre"],
                    Apellido = persona["Apellido"],
                    Edad = Convert.ToInt16(persona["Edad"]),
                    Contraseña = persona["Contraseña"],
                    ConfirmarContraseña = persona["ConfirmarContraseña"]
                };
                miArbol2.Cerrar();
                pelis.peliculasTree2.Cerrar();
                miArbol2.Agregar(model.Edad, model);
            }
            catch
            {
                miArbol2.Cerrar();
                pelis.peliculasTree2.Cerrar();
            }
            miArbol2.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }
        
     
        public ActionResult Catálogo()
        {
            miArbol2.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }
        public ActionResult Login()
        {
            miArbol2.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }
        [HttpPost]
        public ActionResult Login(string usuario, string contraseña)
        {
            
            if (usuario == "admin" && contraseña == "admin")
            {
                miArbol2.Cerrar();
                pelis.peliculasTree2.Cerrar();
                return RedirectToAction("Catálogo", "guaflix");
            }
            
            else
            {
                miArbol2.Cerrar();
                pelis.peliculasTree2.Cerrar();
                return View();
            }
        }
    }
}