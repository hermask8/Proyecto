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
            var datos = textoTamañoFijo.Split('=');
            miPelicula.Tipo = datos[0].Trim();
            miPelicula.Nombre = datos[1].Trim();
            miPelicula.AñoLanzamiento = datos[2].Trim();
            miPelicula.Genero = datos[3].Trim();
            return miPelicula;
        }

        public Peliculas FabricarNulo()
        {
            return new Peliculas();
        }
    }
}