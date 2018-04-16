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
        PeliculasController pelis = new PeliculasController();
        // GET: Guaflix
        public ActionResult Index()
        {
            pelis.miArbol.Cerrar();
            return View();
        }

        public ActionResult ValidarUsiario()
        {
            pelis.miArbol.Cerrar();
            return View();
        }
        
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
                pelis.miArbol.Cerrar();
                //miArbol.Agregar()
            }
            catch
            {
                pelis.miArbol.Cerrar();
            }
            pelis.miArbol.Cerrar();
            return View();
        }
        
     
        public ActionResult Catálogo()
        {
            pelis.miArbol.Cerrar();
            return View();
        }
        public ActionResult Login()
        {
            pelis.miArbol.Cerrar();
            return View();
        }
        [HttpPost]
        public ActionResult Login(string usuario, string contraseña)
        {

            if (usuario == "admin" && contraseña == "admin")
            {
                pelis.miArbol.Cerrar();
                return RedirectToAction("Catálogo", "guaflix");
            }
            else
            {
                pelis.miArbol.Cerrar();
                return View();
            }
            
        }
      


    }
}