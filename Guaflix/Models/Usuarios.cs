using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guaflix.Models
{
    public class Usuarios
    {
       public string Nombre { get; set; }
       public string Apellido { get; set; }
       public int Edad { get; set; }
       public string Contraseña { get; set; }
       public string ConfirmarContraseña { get; set; }
    }
}