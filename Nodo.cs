using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolExpresiones_prueba01
{
    public class Nodo
    {
        #region CAMPOS DE LA CLASE
        private object datos;
        private Nodo nodoIzquierdo;
        private Nodo nodoDerecho;
        #endregion

        #region CONSTRUCTORES
        public Nodo()
        {
            nodoDerecho = nodoIzquierdo = null;
        }
        public Nodo(Object datos)
        {
            this.datos = datos;
            nodoDerecho = nodoIzquierdo = null;
        }
        public Nodo(Nodo derecho,Nodo izquierdo,Object valor)
        {
            this.nodoDerecho = derecho;
            this.nodoIzquierdo = izquierdo;
            this.datos = valor;
        }
        #endregion

        #region PROPIEDADES  CLASE NODO
        //nodo izquierdo
        public Nodo NodoIzquierdo { get => nodoIzquierdo; set => nodoIzquierdo = value; }
        //nodo derecho
        public Nodo NodoDerecho { get => nodoDerecho; set => nodoDerecho = value; }
        //Datos
        public object Datos { get => datos; set => datos = value; }
        #endregion
    }
}
