using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guaflix.Models
{
    public class Usuarios
    {
        string Nombre { get; set; }
        string Apellido { get; set; }
        int Edad { get; set; }
        string Contraseña { get; set; }
        string ConfirmarContraseña { get; set; }
    }
}