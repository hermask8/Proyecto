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
        public ArbolesB.ArbolBusqueda<int, Usuarios> miArbol2 = new ArbolesB.ArbolB<Usuarios>(5, "ArbolB", new FabricarTextoUsuarios());

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
        public ActionResult CrearUsuario()
        {
            miArbol2.Cerrar();
            return View();
        }
        [HttpPost]
        public ActionResult CrearUsuario(FormCollection persona)
        {
            try
            { 
                //Mientras agrega peliculas para que veas que es lo que hace
                //Gabriel XD
                var model = new Usuarios
                {
                    Nombre = persona["Nombre"],
                    Apellido = persona["Apellido"],
                    Edad = Convert.ToInt16(persona["Edad"]),
                    Contraseña = persona["Contraseña"],
                    ConfirmarContraseña = persona["ConfirmarContraseña"]
                };
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