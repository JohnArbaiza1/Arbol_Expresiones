using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using System.Threading;

namespace Prueba_ArbolExpresion
{
    internal class Arbol
    {
        //Region que contiene las propiedades de la clase 
        #region Propiedades de la clase 

        //para la inserccion que se ara en la cola de expresiones
        public string precedencia = "()^*/-+";//Establecemos el orden en que se evaluara cada operador 
        private string[] delimitadores = {"( )","^", "*", "/", "-", "+",};//Delimitadores para la expresion
        private string[] operandoArray;
        private string[] Operadores;
        private Queue colaExpresion;

        //Para la creacion del arbol
        private string token;
        private string operadorTemp;
        private int i = 0;
        private Stack pilaOperadores;
        private Stack pilaOperando;
        private Stack pilaDot;
        private Nodo raiz = null;
        public Nodo nodoDot; 

        //Propiedades para el recorrido 
        public string Preorden;    
        public string Inorden; 
        public string Posorden; 
        #endregion

        //Region que contiene el constructor de la clase 
        #region constructor de la clase

        public Arbol()
        {
            pilaOperadores = new Stack();
            pilaOperando = new Stack();
            pilaDot = new Stack();
            colaExpresion= new Queue();
        }

        #endregion

        //region donde se encuentra el metodo para poder insertar las expresiones 
        #region Insercion a la cola 
        public void Insertar(string Expresion)
        {
            operandoArray = Expresion.Split(delimitadores,StringSplitOptions.RemoveEmptyEntries);
            //Split Devuelve una matriz de cadenas que contiene las subcadenas de una instancia
            //que están delimitadas por elementos de la matriz de cadenas o caracteres
            Operadores = Expresion.Split(operandoArray,StringSplitOptions.RemoveEmptyEntries);
            //StringSplitOptions especifica opciones para las sobrecargas aplicables del metodo split
            for (int i = 0; colaExpresion.Count< operandoArray.Length+(Operadores.Length-1); i++)
            {
                colaExpresion.Enqueue(operandoArray[i]);
                colaExpresion.Enqueue(Operadores[i]);

            }
            colaExpresion.Enqueue(operandoArray[operandoArray.Length-1]);

        }
        #endregion

        #region Arbol

        public Nodo CrearArbol()
        {
            while (colaExpresion.Count != 0)
            {
                token = (string)colaExpresion.Dequeue();
                if (precedencia.IndexOf(token) < 0)
                {
                    pilaOperando.Push(new Nodo(token));
                    pilaDot.Push(new Nodo("nodo{++i}[label=\"{token}\"]"));
                }
                else
                {
                    if (pilaOperadores.Count != 0)
                    {
                        operadorTemp = (string)pilaOperadores.Peek();
                        while(pilaOperadores.Count !=0 && precedencia.IndexOf(operadorTemp) >= precedencia.IndexOf(token))
                        {
                            GuardaSubArbol();
                            if (pilaOperadores.Count != 0)
                            {
                                operadorTemp=(string )pilaOperadores.Peek();
                            }

                        }
                    }
                    pilaOperadores.Push(token);
                }          
            }

            raiz = (Nodo)pilaOperando.Peek();
            nodoDot = (Nodo)pilaDot.Peek();
            while (pilaOperadores.Count !=0)
            {
                GuardaSubArbol();
                raiz = (Nodo)pilaOperando.Peek();
                nodoDot = (Nodo)pilaDot.Peek();

            }
            return raiz;
        }

        private void GuardaSubArbol()
        {
            Nodo nDerecho = (Nodo)pilaOperando.Pop();
            Nodo nIzquierdo = (Nodo)pilaOperando.Pop();
            pilaOperando.Push(new Nodo(nDerecho,nIzquierdo,pilaOperadores.Peek()));

            Nodo derechoG =(Nodo)pilaDot.Pop();
            Nodo IzquierdoG= (Nodo)pilaDot.Pop();
            pilaDot.Push(new Nodo(derechoG, IzquierdoG, $"nodo{i++}label=\"{pilaOperadores.Pop()}\"]"));

        }

        #endregion


        //Region donde se encuentran los metodos de recorrido en preorden,Inoreden y posorden
        #region Recorridos

        public string InsertarPre(Nodo arbol)
        {
            if(arbol != null)
            {
                Preorden += arbol.expresion + " ";
                InsertarPre(arbol.NoIzquierdo);
                InsertarPre(arbol.NoDerecho);
            }
            return Preorden;
        }

        public string InsertarIn(Nodo arbol)
        {
            if (arbol != null)
            {
                InsertarIn(arbol.NoIzquierdo);
                Inorden += arbol.expresion + " ";             
                InsertarIn(arbol.NoDerecho);
            }
            return Inorden;
        }

        public string InsertarPos(Nodo arbol)
        {
            if (arbol != null)
            {
                
                InsertarPos(arbol.NoIzquierdo);
                InsertarPos(arbol.NoDerecho);
                Posorden += arbol.expresion + " ";
            }
            return Posorden;
        }

        //Metodo que se usara para limpiar
        public void Limpiar()
        {
            Preorden = " ";
            Inorden = " ";
            Posorden = " ";
        }

        #endregion


        public void DibujarArbol(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, Brush encuentro)
        {
            //Coordenadas iniciales 
            int x = 400; // Posiciones de la raíz del árbol
            int y = 75;
            if (raiz == null)
                return;
            raiz.PosicionNodo(ref x, y); //Posición de cada nodo
            raiz.DibujarRamas(grafo, Lapiz); //Dibuja los Enlaces entre nodos
                                             //Dibuja todos los Nodos
            raiz.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, encuentro);//Crea las elipsis y las rellena 
        }
        public int x1 = 400;
        // Posiciones iniciales de la raíz del árbol
        public int y2 = 75;
        // Función para Colorear los nodos
        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, Nodo Raiz, bool post, bool inor, bool preor)
        {
            Brush entorno = Brushes.Red;

            //En los if se virifica si se especifico que el arbol se dibuje en inor, preor o en post
            if (inor == true)
            {
                if (Raiz != null)
                {
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NoIzquierdo, post, inor, preor);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(1000);
                    // pausar la ejecución 1000 milisegundos
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NoDerecho, post, inor, preor);
                }
            }
            else
            if (preor == true)
            {
                if (Raiz != null)
                {
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(1000);
                    // pausar la ejecución 1000 milisegundos
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NoIzquierdo, post,
                     inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NoDerecho, post, inor, preor);
                }
            }
            else if (post == true)
            {
                if (Raiz != null)
                {
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NoIzquierdo, post, inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NoDerecho, post, inor, preor);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(1000); // pausar la ejecución 1000 milisegundos
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                }
            }
        }

    }
}
