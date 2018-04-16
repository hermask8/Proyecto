using Guaflix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guaflix.TamañoFijo
{
    public class FabricarTexto : ArbolesB.IFabricaTextoTamañoFijo<Peliculas>
    {
        public Peliculas Fabricar(string textoTamañoFijo)
        {
            Peliculas miPelicula  = new Peliculas();
            var datos = textoTamañoFijo.Split('/');

            miPelicula.Tipo = datos[0].PadLeft(20, '%');
            miPelicula.Nombre = datos[1].PadLeft(20, '%');
            miPelicula.AñoLanzamiento = int.Parse(datos[2].PadLeft(20, '0'));
            miPelicula.Genero = datos[3].PadLeft(20, '%');
            return miPelicula;
        }

        public Peliculas FabricarNulo()
        {
            return new Peliculas();
        }
    }
}