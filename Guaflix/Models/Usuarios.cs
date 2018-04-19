using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Guaflix.Models
{
    public class Usuarios : ArbolesB.ITextoTamañoFijo, IComparable
    {
       private const string TextoEnteroFormato = "%%%%%%%%%%%%%%%%%%%%=%%%%%%%%%%%%%%%%%%%%=%%%%%%%%%%%%%%%=%%%=%%%%%%%%%%%%%%%%%%%%=%%%%%%%%%%%%%%%%%%%%";

       public string username { get; set; }
       public string nombre { get; set; }
       public string apellido { get; set; }
       public string edad { get; set; }
       public string password { get; set; }
       public string confirmapassword { get; set; }

        public Usuarios()
        {
            username = "";
            nombre = "";
            apellido = "";
            edad = "";
            password = "";
            confirmapassword = "";

        }
        public Usuarios(string Username,string Nombre, string Apellido, string Edad, string contraseña, string confirmarcontraseña)
        {
            username = Username;
            nombre = Nombre;
            Apellido = apellido;
            edad = Edad;
            password = contraseña;
            confirmapassword = confirmarcontraseña;
        }

        public int TamañoEnTexto
        {
            get
            {
                return 102 ;
            }
        }

        public string ConvertirATextoTamañoFijo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(username.PadLeft(20, '%'));
            sb.Append('=');
            sb.Append(nombre.PadLeft(20, '%'));
            sb.Append('=');
            sb.Append(apellido.PadLeft(15, '%'));
            sb.Append('=');
            sb.Append(edad.PadLeft(3, '%'));
            sb.Append('=');
            sb.Append(password.PadLeft(20, '%'));
            sb.Append('=');
            sb.Append(password.PadLeft(20, '%'));
            return sb.ToString();
        }

        public int CompareTo(Object theObject)
        {
            string Total = nombre.ToString() + apellido.ToString() + edad.ToString() + password.ToString() + confirmapassword.ToString();
            return theObject.ToString().CompareTo(Total);
        }

        public override string ToString()
        {
            return ConvertirATextoTamañoFijo();
        }
    }
}
