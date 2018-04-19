using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Guaflix.Models;

namespace Guaflix.TamañoFijo
{
    public class FabricarTextoUsuarios : ArbolesB.IFabricaTextoTamañoFijo<Usuarios>
    {
        public Usuarios Fabricar(string textoTamañoFijo)
        {
            Usuarios miUsuario = new Usuarios();
            var datos = textoTamañoFijo.Split('=');
            miUsuario.username = datos[0].PadLeft(20, '%');
            miUsuario.nombre = datos[1].PadLeft(20, '%');
            miUsuario.apellido = datos[2].PadLeft(15, '%');
            miUsuario.edad = datos[3].PadLeft(3, '%');
            miUsuario.password = datos[4].PadLeft(20, '%');
            miUsuario.confirmapassword = datos[5].PadLeft(20, '%');
            return miUsuario;
        }

        public Usuarios FabricarNulo()
        {
            return new Usuarios();
        }
    }
}