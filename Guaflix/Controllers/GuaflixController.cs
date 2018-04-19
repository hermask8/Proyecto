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
using ArbolesB;

namespace Guaflix.Controllers
{
    public class GuaflixController : Controller
    {
        PeliculasController pelis = new PeliculasController();
        public static List<Usuarios2> PeliculaLista = new List<Usuarios2>();
        
        public ArbolB<Usuarios> UsersTree = new ArbolB<Usuarios>(3, "users.tree", new FabricarTextoUsuarios());
        // GET: Guaflix
        public void Obtener()
        {
            List<string> miListado2 = new List<string>();
            miListado2 = UsersTree.miLIstado();
            foreach (var item in miListado2)
            {
                var valores = item.Split('=');
                var modelo2 = new Usuarios2
                {
                    username = valores[0].Trim('%'),
                    nombre = valores[1].Trim('%'),
                    apellido = valores[2].Trim('%'),
                    edad = valores[3].Trim('%'),
                    password = valores[4].Trim('%'),
                    confirmapassword = valores[5].Trim('%'),
                };
                PeliculaLista.Add(modelo2);
            }

        }

        public ActionResult Index()
        {
            UsersTree.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }

        public ActionResult ValidarUsuario()
        {
            UsersTree.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }

        public ActionResult CargarUsuarios()
        {
            UsersTree.Cerrar();
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
                    UsersTree.Agregar(info.nombre, info);
                    i++;
                }
                UsersTree.Cerrar();
                pelis.peliculasTree2.Cerrar();
            }
            UsersTree.Cerrar();
            return View();
        }
        public ActionResult CrearUsuario()
        {
            UsersTree.Cerrar();
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
                    username = persona["username"],
                    nombre = persona["nombre"],
                    apellido = persona["apellido"],
                    edad = persona["edad"],
                    password = persona["password"],
                    confirmapassword = persona["confirmpassword"]
                };
                
                pelis.peliculasTree2.Cerrar();
                UsersTree.Agregar(model.nombre, model);
                UsersTree.Cerrar();
            }
            catch
            {
                UsersTree.Cerrar();
                pelis.peliculasTree2.Cerrar();
            }
            UsersTree.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }
        
     
        public ActionResult Catálogo()
        {
            UsersTree.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }
        public ActionResult Login()
        {
            UsersTree.Cerrar();
            pelis.peliculasTree2.Cerrar();
            return View();
        }
        [HttpPost]
        public ActionResult Login(string usuario, string contraseña)
        {
            
            if (usuario == "admin" && contraseña == "admin")
            {
                UsersTree.Cerrar();
                pelis.peliculasTree2.Cerrar();
                return RedirectToAction("Catálogo", "guaflix");
            }
            
            else
            {
                UsersTree.Cerrar();
                pelis.peliculasTree2.Cerrar();
                return View();
            }
        }
    }
}