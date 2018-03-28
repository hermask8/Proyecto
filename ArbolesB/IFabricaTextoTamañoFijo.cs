using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolesB
{
    public interface IFabricaTextoTamañoFijo<T> where T: ITextoTamañoFijo
    {
        T Fabricar(string textoTamañoMismo);
        T FabricarNulo();
    }
}
