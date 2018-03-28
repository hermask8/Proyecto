﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArbolesB
{
    internal class NodoB<T> where T : ITextoTamañoFijo
    {
        //orden minimo del arbol
        public const int OrdenMinimo = 5;
        //Orden maximo del arbol
        public const int OrdenMaximo = 99;
        //Orden del arbol
        internal int Orden { get; private set; }
        //Obtiene la posicion del nodo
        internal int Posicion { get; private set; }
        //Padre de un nodo
        internal int Padre { get; set; }
        //Listado de hijos de un nodo
        internal List<int> Hijos { get; set; }
        //Listado de llaves de un nodo
        internal List<int> Llaves { get; set; }
        //Listado de datos de un nodo
        internal List<T> Datos { get; set; }

        //retorna la cantidad de datos de un nodo, apuntador vacio es un dato y es -1
        internal int CantidadDatos
        {
            get
            {
                int i = 0;
                while (i < Llaves.Count && Llaves[i] != Utilidades.ApuntadorVacio)
                {
                    i++;
                }
                return i;
            }
        }
        //Retorna verdadero si no tiene la cantidad minima de datos o falso si tiene la cantidad minima o mas datos
        internal bool Underflow
        {
            get
            {
                return (CantidadDatos < ((Orden / 2) - 1));
            }
        }

        //retorna verdadero verdadero si está lleno el nodo o falso si no lo está
        internal bool Lleno
        {
            get
            {
                return (CantidadDatos >= Orden - 1);
            }
        }
        //Retorna si el nodo en el que esta es hoja
        internal bool EsHoja
        {
            get
            {
                bool EsHoja = true;

                for (int i = 0; i < Hijos.Count; i++)
                {
                    if (Hijos[i] != Utilidades.ApuntadorVacio)
                    {
                        EsHoja = false;
                        break;
                    }
                }
                return EsHoja;
            }
        }

        //Es el tamaño de digitos de una linea completa del archivo txt
        internal int TamañoEnTexto
        {
            get
            {
                int tamañoEnTexto = 0;

                tamañoEnTexto += Utilidades.TextoEnteroTamaño + 1; // Tamaño del indicador de Posición               
                tamañoEnTexto += Utilidades.TextoEnteroTamaño + 1; // Tamaño apuntador al Padre    
                tamañoEnTexto += 2; // Separadores adicionales              
                tamañoEnTexto += (Utilidades.TextoEnteroTamaño + 1) * Orden; // Tamaño Hijos           
                tamañoEnTexto += 2; // Separadores adicionales       
                tamañoEnTexto += (Utilidades.TextoEnteroTamaño + 1) * (Orden - 1); // Tamaño Llaves 
                tamañoEnTexto += 2; // Separadores adicionales        
                tamañoEnTexto += (Datos[0].TamañoEnTexto + 1) * (Orden - 1); // Tamaño Datos                 
                tamañoEnTexto += Utilidades.TextoNuevaLineaTamaño; // Tamaño del Enter 

                return tamañoEnTexto;
            }
        }
        //retorna el tamaño en bytes
        internal int TamañoEnBytes
        {
            get
            {
                return TamañoEnTexto * Utilidades.BinarioCaracterTamaño;
            }
        }

        //calcula el nodo en disco
        private int CalcularPosicionEnDisco(int tamañoEncabezado)
        {
            return tamañoEncabezado + (Posicion * TamañoEnBytes);
        }

        //convierte el dato ingresado a un tamaño especifico
        private string ConvertirATextoTamañoFijo()
        {
            StringBuilder datosCadena = new StringBuilder();

            datosCadena.Append(Utilidades.FormatearEntero(Posicion));
            datosCadena.Append(Utilidades.TextoSeparador);

            datosCadena.Append(Utilidades.FormatearEntero(Padre));
            datosCadena.Append(Utilidades.TextoSeparador);

            datosCadena.Append(Utilidades.TextoSeparador);
            datosCadena.Append(Utilidades.TextoSeparador);

            for (int i = 0; i < Hijos.Count; i++)
            {
                datosCadena.Append(Utilidades.FormatearEntero(Hijos[i]));
                datosCadena.Append(Utilidades.TextoSeparador);
            }

            datosCadena.Append(Utilidades.TextoSeparador);
            datosCadena.Append(Utilidades.TextoSeparador);

            for (int i = 0; i < Llaves.Count; i++)
            {
                datosCadena.Append(Utilidades.FormatearEntero(Llaves[i]));
                datosCadena.Append(Utilidades.TextoSeparador);
            }

            datosCadena.Append(Utilidades.TextoSeparador);
            datosCadena.Append(Utilidades.TextoSeparador);

            for (int i = 0; i < Datos.Count; i++)
            {
                datosCadena.Append(Datos[i].ConvertirATextoTamañoFijo().Replace(Utilidades.TextoSeparador, Utilidades.TextoSustitutoSeparador));
                datosCadena.Append(Utilidades.TextoSeparador);
            }

            datosCadena.Append(Utilidades.TextoNuevaLinea);

            return datosCadena.ToString();
        }

        //convierte el dato ingresado en bytes
        private byte[] ObtenerBytes()
        {
            byte[] datosBinarios = null;
            datosBinarios = Utilidades.ConvertirBinarioYTexto(ConvertirATextoTamañoFijo());
            return datosBinarios;
        }

        //Limpia un nodo para poder inicializarlos
        private void LimpiarNodo(IFabricaTextoTamañoFijo<T> fabrica)
        {
            Hijos = new List<int>();
            for (int i = 0; i < Orden; i++)
            {
                Hijos.Add(Utilidades.ApuntadorVacio);
            }

            Llaves = new List<int>();
            for (int i = 0; i < Orden - 1; i++)
            {
                Llaves.Add(Utilidades.ApuntadorVacio);
            }

            Datos = new List<T>();
            for (int i = 0; i < Orden - 1; i++)
            {
                Datos.Add(fabrica.FabricarNulo());
            }
        }

        //
        internal NodoB(int orden, int posicion, int padre, IFabricaTextoTamañoFijo<T> fabrica)
        {
            if ((orden < OrdenMinimo) || (orden > OrdenMaximo))
            {
                throw new ArgumentOutOfRangeException("orden");
            }

            if (posicion < 0)
            {
                throw new ArgumentOutOfRangeException("posicion");
            }

            Orden = orden;
            Posicion = posicion;
            Padre = padre;

            LimpiarNodo(fabrica);
        }

        internal static NodoB<T> LeerNodoDesdeDisco(FileStream archivo, int tamañoEncabezado, int orden, int posicion, IFabricaTextoTamañoFijo<T> fabrica)
        {
            if (archivo == null) { throw new ArgumentNullException("archivo"); }

            if (tamañoEncabezado < 0) { throw new ArgumentOutOfRangeException("tamañoEncabezado"); }

            if ((orden < OrdenMinimo) || (orden > OrdenMaximo)) { throw new ArgumentOutOfRangeException("orden"); }

            if (posicion < 0) { throw new ArgumentOutOfRangeException("posicion"); }
            if (fabrica == null) { throw new ArgumentNullException("fabrica"); }
            // Se crea un nodo nulo para poder acceder a las          
            // propiedades de tamaño calculadas sobre la instancia 
            // el dato de la instancia del nodo             
            NodoB<T> nuevoNodo = new NodoB<T>(orden, posicion, 0, fabrica);

            // Se crea un buffer donde se almacenarán los bytes leidos           
            byte[] datosBinario = new byte[nuevoNodo.TamañoEnBytes];

            // Variables a ser utilizadas luego de que el archivo sea leido          
            string datosCadena = ""; string[] datosSeparados = null; int PosicionEnDatosCadena = 1;

            // Se ubica la posición donde deberá estar el nodo y se lee desde el archivo           
            archivo.Seek(nuevoNodo.CalcularPosicionEnDisco(tamañoEncabezado), SeekOrigin.Begin);
            archivo.Read(datosBinario, 0, nuevoNodo.TamañoEnBytes);

            // Se convierten los bytes leidos del archivo a una cadena         
            datosCadena = Utilidades.ConvertirBinarioYTexto(datosBinario);

            // Se quitan los saltos de línea y se separa en secciones        
            datosCadena = datosCadena.Replace(Utilidades.TextoNuevaLinea, "");
            datosCadena = datosCadena.Replace("".PadRight(3, Utilidades.TextoSeparador), Utilidades.TextoSeparador.ToString());
            datosSeparados = datosCadena.Split(Utilidades.TextoSeparador);

            // Se se obtiene la posición del Padre       
            nuevoNodo.Padre = Convert.ToInt32(datosSeparados[PosicionEnDatosCadena]);
            PosicionEnDatosCadena++;

            // Se asignan al nodo vacío los hijos desde la cadena separada             
            for (int i = 0; i < nuevoNodo.Hijos.Count; i++)
            {
                nuevoNodo.Hijos[i] = Convert.ToInt32(datosSeparados[PosicionEnDatosCadena]);
                PosicionEnDatosCadena++;
            }

            // Se asignan al nodo vacío las llaves desde la cadena separada 
            for (int i = 0; i < nuevoNodo.Llaves.Count; i++)
            {
                nuevoNodo.Llaves[i] = Convert.ToInt32(datosSeparados[PosicionEnDatosCadena]);
                PosicionEnDatosCadena++;
            }

            // Se asignan al nodo vacío los datos la cadena separada            
            for (int i = 0; i < nuevoNodo.Datos.Count; i++)
            {
                datosSeparados[PosicionEnDatosCadena] = datosSeparados[PosicionEnDatosCadena].Replace(Utilidades.TextoSustitutoSeparador, Utilidades.TextoSeparador);
                nuevoNodo.Datos[i] = fabrica.Fabricar(datosSeparados[PosicionEnDatosCadena]);
                PosicionEnDatosCadena++;
            }

            // Se retorna el nodo luego de agregar toda la información   
            return nuevoNodo;
        }

        internal void GuardarNodoEnDisco(FileStream archivo, int tamañoEncabezado)
        {
            // Se ubica la posición donde se debe escribir        
            archivo.Seek(CalcularPosicionEnDisco(tamañoEncabezado), SeekOrigin.Begin);

            // Se escribe al archivo y se fuerza a vaciar el buffer       
            archivo.Write(ObtenerBytes(), 0, TamañoEnBytes);
            archivo.Flush();
        }

        internal void LimpiarNodoEnDisco(FileStream archivo, int tamañoEncabezado, IFabricaTextoTamañoFijo<T> fabrica)
        {
            // Se limpia el contenido del nodo         
            LimpiarNodo(fabrica);

            // Se guarda en disco el objeto que ha sido limpiado  
            GuardarNodoEnDisco(archivo, tamañoEncabezado);
        }

        internal int PosicionAproximadaEnNodo(int llave)
        {
            int posicion = Llaves.Count;

            for (int i = 0; i < Llaves.Count; i++)
            {
                if ((Llaves[i] > llave) || (Llaves[i] == Utilidades.ApuntadorVacio))
                {
                    posicion = i;
                    break;
                }
            }

            return posicion;
        }



        internal int PosicionExactaEnNodo(int llave)
        {
            int posicion = -1;
            for (int i = 0; i < Llaves.Count; i++)
            {
                if (llave == Llaves[i])
                {
                    posicion = i;
                }
                break;
            }

            return posicion;
        }

        internal void AgregarDato(int llave, T dato, int hijoDerecho)
        {
            AgregarDato(llave, dato, hijoDerecho, true);
        }


        internal void AgregarDato(int llave, T dato, int hijoDerecho, bool ValidarLleno)
        {
            if (Lleno && ValidarLleno)
            {
                throw new IndexOutOfRangeException("El nodo está lleno, ya no puede insertar más datos");
            }

            if (llave == Utilidades.ApuntadorVacio)
            {
                throw new ArgumentOutOfRangeException("llave");
            }

            // Se ubica la posición para insertar, en el punto              
            // donde se encuentre el primer registro mayor que la llave       
            int posicionParaInsertar = 0;
            posicionParaInsertar = PosicionAproximadaEnNodo(llave);

            // Corrimiento de hijos
            for (int i = Hijos.Count - 1; i > posicionParaInsertar + 1; i--)
            {
                Hijos[i] = Hijos[i - 1];
            }
            Hijos[posicionParaInsertar + 1] = hijoDerecho;

            // Corrimiento de llaves        
            for (int i = Llaves.Count - 1; i > posicionParaInsertar; i--)
            {
                Llaves[i] = Llaves[i - 1];
                Datos[i] = Datos[i - 1];
            }
            Llaves[posicionParaInsertar] = llave;
            Datos[posicionParaInsertar] = dato;
        }


        internal void AgregarDato(int llave, T dato)
        {
            AgregarDato(llave, dato, Utilidades.ApuntadorVacio);
        }


        internal void EliminarDato(int llave)
        {
            if (!EsHoja)
            {
                throw new Exception("Solo pueden eliminarse llaves en nodos hoja");
            }

            // Se ubica la posición para eliminar, en el punto           
            // donde se encuentre el registro igual a la llave    
            int posicionParaEliminar = -1;
            posicionParaEliminar = PosicionExactaEnNodo(llave);

            // La llave no está contenida en el nodo 
            if (posicionParaEliminar == -1)
            {
                throw new ArgumentException("No puede eliminarse ya que no existe la llave en el nodo");
            }

            // Corrimiento de llaves y datos            
            for (int i = Llaves.Count - 1; i > posicionParaEliminar; i--)
            {
                Llaves[i - 1] = Llaves[i];
                Datos[i - 1] = Datos[i];
            }
            Llaves[Llaves.Count - 1] = Utilidades.ApuntadorVacio;
        }


        internal void SepararNodo(int llave, T dato, int hijoDerecho, NodoB<T> nuevoNodo, ref int llavePorSubir, T datoPorSubir)
        {
            if (!Lleno)
            {
                throw new Exception("Uno nodo solo puede separarse si está lleno");
            }

            // Incrementar el tamaño de las listas en una posición       
            Llaves.Add(Utilidades.ApuntadorVacio);
            Datos.Add(dato);
            Hijos.Add(Utilidades.ApuntadorVacio);

            // Agregar los nuevos elementos en orden   
            AgregarDato(llave, dato, hijoDerecho, false);

            // Obtener los valores a subir     
            int mitad = (Orden / 2);
            llavePorSubir = Llaves[mitad];
            datoPorSubir = Datos[mitad];
            Llaves[mitad] = Utilidades.ApuntadorVacio;

            // Llenar las llaves y datos que pasan al nuevo nodo       
            int j = 0;
            for (int i = mitad + 1; i < Llaves.Count; i++)
            {
                nuevoNodo.Llaves[j] = Llaves[i];
                nuevoNodo.Datos[j] = Datos[i];
                Llaves[i] = Utilidades.ApuntadorVacio;
                j++;
            }

            // Llenar los hijos que pasan al nuevo nodo       
            j = 0;
            for (int i = mitad + 1; i < Hijos.Count; i++)
            {
                nuevoNodo.Hijos[j] = Hijos[i];
                 Hijos[i] = Utilidades.ApuntadorVacio; 
                j++;
            }
            Llaves.RemoveAt(Llaves.Count - 1);
            Datos.RemoveAt(Datos.Count - 1);
            Hijos.RemoveAt(Hijos.Count - 1);
        } 
 
 
    }
}