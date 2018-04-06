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
            miPelicula.Tipo = textoTamañoFijo.Substring(11, 1);
            miPelicula.Nombre = textoTamañoFijo.Substring(11, 1);
            miPelicula.AñoLanzamiento = Convert.ToInt32(textoTamañoFijo.Substring(0,11));
            miPelicula.Genero = textoTamañoFijo.Substring(0,11);
            return miPelicula;
        }

        public Peliculas FabricarNulo()
        {
            return new Peliculas();
        }
    }
}