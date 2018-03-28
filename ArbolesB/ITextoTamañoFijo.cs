using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolesB
{
    public interface ITextoTamañoFijo
    {
        int TamañoEnTexto { get; }
        string ConvertirATextoTamañoFijo();
    }
}
