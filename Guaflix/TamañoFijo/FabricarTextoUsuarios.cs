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
            var datos = textoTamañoFijo.Split('/');

            miUsuario.Nombre = datos[0].PadLeft(10, '$');
            miUsuario.Apellido = datos[1].PadLeft(15, '$');
            miUsuario.Edad = int.Parse(datos[2].PadLeft(3, '0'));
            miUsuario.Contraseña = datos[3].PadLeft(20, '$');
            miUsuario.ConfirmarContraseña = datos[4].PadLeft(20, '$');
            return miUsuario;
        }

        public Usuarios FabricarNulo()
        {
            return new Usuarios();
        }
    }
}