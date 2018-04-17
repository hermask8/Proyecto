using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Guaflix.Models
{
    public class Peliculas: ArbolesB.ITextoTamañoFijo, IComparable
    {
        private const string TextoEnteroFormato = "%%%%%%%%%%%%%%%%%%%%/%%%%%%%%%%%%%%%%%%%%/00000000000000000000/%%%%%%%%%%%%%%%%%%%%";
        public string Nombre { get; set; }
        public int AñoLanzamiento { get; set; }
        public string Tipo { get; set; }
        public string Genero { get; set; }
        
        public Peliculas()
        {
            Tipo = "";
            Nombre = "";
            AñoLanzamiento = 0;
            Genero = "";
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
                return 83;
            }
        }

        public string ConvertirATextoTamañoFijo()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Tipo.PadLeft(20, '%'));
            sb.Append('/');
            sb.Append(Nombre.PadLeft(20, '%'));
            sb.Append('/');
            sb.Append(Convert.ToString(AñoLanzamiento).PadLeft(20, '0'));
            sb.Append('/');
            sb.Append(Genero.PadLeft(20, '%'));

            return sb.ToString();
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