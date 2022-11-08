using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolExpresiones_prueba01
{
    public class Arbol
    {
        #region CAMPOS DO CLASE
        //Insercion
        private string precedencia = "+-*/^";
        private string[] delimitadores = { "+", "-", "*", "/", "^" };
        private string[] operandosArray;
        private string[] operadoresArray;
        private Queue colaExpresion;

        //creando arbol

        private string token;
        private string operadorTemp;
        private int i = 0;
        private Stack piladeOperadores;
        private Stack pilaOperandos;
        private Stack pilaDot;
        private Nodo raiz = null;
        public Nodo nodoDot { get; set; }

        //Propiedades de los recorridos
        public string cadenaPreorden { get; set; }
        public string cadenaInorden { get; set; }
        public string cadenaPosorden { get; set; }
        #endregion

        #region CONSTRUCTORES
        public Arbol()
        {
            piladeOperadores = new Stack();
            pilaOperandos = new Stack();
            pilaDot = new Stack();
            colaExpresion = new Queue();
        }
        #endregion

        #region Insercion
        public void InsertarEnCola(string expresion)
        {
            operadoresArray = expresion.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            operadoresArray = expresion.Split(operadoresArray, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; colaExpresion.Count < operadoresArray.Length+(operadoresArray.Length-1); i++)
            {
                colaExpresion.Enqueue(operandosArray[i]);
                colaExpresion.Enqueue(operadoresArray[i]);
            }
            colaExpresion.Enqueue(operandosArray[operandosArray.Length - 1]);

        }
        #endregion

        #region ARBOL
        public Nodo CrearArbol()
        {
            while (colaExpresion.Count !=0)
            {
                token = (string)colaExpresion.Dequeue();
                if (precedencia.IndexOf(token)<0)
                {
                    pilaOperandos.Push(new Nodo(token));
                    pilaDot.Push(new Nodo($"nodo{++i}[label=\"{token}\"]"));
                }
                else
                {
                    if (piladeOperadores.Count != 0)
                    {
                        operadorTemp = (string)piladeOperadores.Peek();
                        while (piladeOperadores.Count !=0 && precedencia.IndexOf(operadorTemp)>=precedencia.IndexOf(token))
                        {
                            GuardarSubArbol();
                            if (piladeOperadores.Count !=0)
                            {
                                operadorTemp = (string)piladeOperadores.Peek();
                            }
                        }
                    }
                    piladeOperadores.Push(token);
                }
            }
            raiz = (Nodo)pilaOperandos.Peek();
            nodoDot = (Nodo)pilaDot.Peek();
            while (piladeOperadores.Count !=0)
            {
                GuardarSubArbol();
                raiz = (Nodo)pilaOperandos.Peek();
                nodoDot = (Nodo)pilaDot.Peek();
            }
            return raiz;
        }

        private void GuardarSubArbol()
        {
            Nodo derecho = (Nodo)piladeOperadores.Pop();
            Nodo izquierdo = (Nodo)pilaOperandos.Pop();
            pilaOperandos.Push(new Nodo(derecho, izquierdo, piladeOperadores.Peek()));

            Nodo derechoG = (Nodo)pilaDot.Pop();
            Nodo izquierdoG = (Nodo)pilaDot.Pop();
            pilaDot.Push(new Nodo(derechoG, izquierdoG, $"nodo{++i}[label=\"{piladeOperadores.Pop()}\"]"));

        }
        #endregion

        #region RECORRIDOS
        //Preorden
        public string InsertarPre(Nodo tree)
        {
            if (tree != null)
            {
                cadenaPreorden += tree.Datos + " ";
                InsertarPre(tree.NodoIzquierdo);
                InsertarPre(tree.NodoDerecho);
            }
            return cadenaPreorden;
        }
        //Inorden
        public string InsertarIn(Nodo tree)
        {
            if (tree != null)
            {
                cadenaInorden += tree.Datos + " ";
                InsertarIn(tree.NodoIzquierdo);
                InsertarIn(tree.NodoDerecho);
            }
            return cadenaInorden;
        }
        //PostOrden
        public string InsertarPost(Nodo tree)
        {
            if (tree != null)
            {
                cadenaPosorden += tree.Datos + " ";
                InsertarPost(tree.NodoIzquierdo);
                InsertarPost(tree.NodoDerecho);
            }
            return cadenaPosorden;
        }
        #endregion
        public void Limpiar()
        {
            cadenaPreorden = " ";
            cadenaInorden = " ";
            cadenaPosorden = " ";
        }
    }
}
