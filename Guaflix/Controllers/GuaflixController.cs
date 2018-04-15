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
        ArbolesB.ArbolBusqueda<int, Peliculas> miArbol = new ArbolesB.ArbolB<Peliculas>(5, "TreeB", new FabricarTexto());
        // GET: Guaflix
        public ActionResult Index()
        {
            miArbol.Cerrar();
            return View();
        }

        public ActionResult ValidarUsiario()
        {
            return View();
        }
        PeliculasController pelis = new PeliculasController();
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

                //miArbol.Agregar()
            }
            catch
            {

            }
            miArbol.Cerrar();
            return View();
        }
        
     
        public ActionResult Catálogo()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string usuario, string contraseña)
        {
            if (usuario == "admin" && contraseña == "admin")
            {  
                return RedirectToAction("Catálogo", "guaflix");
            }
            else
            {
                return View();
            }
           
        }
      


    }
}