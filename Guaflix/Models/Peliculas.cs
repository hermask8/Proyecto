using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guaflix.Models
{
    public class Peliculas: ArbolesB.ITextoTamañoFijo, IComparable
    {
        private const string TextoEnteroFormato = "00000000000;-0000000000";
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public int AñoLanzamiento { get; set; }
        public string Genero { get; set; }

        public Peliculas()
        {
            Tipo = "0";
            Nombre = "0";
            AñoLanzamiento = 0;
            Genero = "0";
        }

        public Peliculas(string tipo, string nombre, int año, string genero)
        {
            Tipo = tipo;
            Nombre = nombre;
            AñoLanzamiento = año;
            Genero = genero;
        }

        public int TamañoEnTexto
        {
            get
            {
                return 12;
            }
        }

        public string ConvertirATextoTamañoFijo()
        {
            return Tipo.ToString() + Nombre.ToString() + AñoLanzamiento.ToString(TextoEnteroFormato) + Genero.ToString();
        }

        public int CompareTo(Object theObject)
        {
            string Total = Tipo.ToString() + Nombre.ToString() + AñoLanzamiento.ToString() + Genero.ToString() ;

            return theObject.ToString().CompareTo(Total);
        }

        public override string ToString()
        {
            return ConvertirATextoTamañoFijo();
        }
    }
}