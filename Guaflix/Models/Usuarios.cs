using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Guaflix.Models
{
    public class Usuarios : ArbolesB.ITextoTamañoFijo, IComparable
    {
        private const string TextoEnteroFormato = "$$$$$$$$$$/$$$$$$$$$$$$$$$/000/$$$$$$$$$$$$$$$$$$$$/$$$$$$$$$$$$$$$$$$$$";
       public string Nombre { get; set; }
       public string Apellido { get; set; }
       public int Edad { get; set; }
       public string Contraseña { get; set; }
       public string ConfirmarContraseña { get; set; }

        public Usuarios()
        {
            Nombre = "";
            Apellido = "";
            Edad = 0;
            Contraseña = "";
            ConfirmarContraseña = "";

        }
        public Usuarios(string nombre, string apellido, int edad, string contraseña, string confirmarcontraseña)
        {
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Contraseña = contraseña;
            ConfirmarContraseña = confirmarcontraseña;
        }

        public int TamañoEnTexto
        {
            get
            {
                return 72 ;
            }
        }

        public string ConvertirATextoTamañoFijo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Nombre.PadLeft(10, '$'));
            sb.Append('/');
            sb.Append(Apellido.PadLeft(15, '$'));
            sb.Append('/');
            sb.Append(Convert.ToString(Edad).PadLeft(3, '0'));
            sb.Append('/');
            sb.Append(Contraseña.PadLeft(20, '$'));
            sb.Append('/');
            sb.Append(ConfirmarContraseña.PadLeft(20, '$'));
            return sb.ToString();
        }

        public int CompareTo(Object theObject)
        {
            string Total = Nombre.ToString() + Apellido.ToString() + Edad.ToString() + Contraseña.ToString() + ConfirmarContraseña.ToString();
            return theObject.ToString().CompareTo(Total);
        }

        public override string ToString()
        {
            return ConvertirATextoTamañoFijo();
        }
    }
}
