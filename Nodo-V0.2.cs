using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_ArbolExpresion
{
    internal class Nodo
    {

        //Region que contiene las propiedades empleadas en la clase
        #region Propiedades de la clase 

        //Establecemos una variable de tipo objeto para la expresion
        public  Object expresion;
        //Variables de tipo Nodo
        public Nodo NoIzquierdo;
        public Nodo NoDerecho;

        //********************************************************************

        //Propiedades para la parte grafica de nuestro arbol de expresiones
        public Rectangle prueba;
        private const int nRadio = 30;//Variable para el manejo de distancia horizontal
        private const int nDistanciaH = 40;//variable para el manejo de distancia vertical
        private const int nDistanciaV = 10;
        //Variables para manejar las posiciones de los ejes X y Y
        private int nCoordenadaX;
        private int nCoordenadaY;
        Graphics col;   
        #endregion

        //***************************************************************

        //Region donde se encuentran nuestros constructores 
        #region Constructores de la clase

        //Constructor solo con el parametro de expresion
        public Nodo(object expre)
        {
            this.expresion = expre;
            NoDerecho = NoIzquierdo = null;
        }

        //Constructor con todos los parametros 
        public Nodo(Nodo nDerecho,Nodo nIzquierdo, object nueva_expresion)
        {
            this.NoDerecho = nDerecho;
            this.NoIzquierdo = nIzquierdo;   
            this.expresion = nueva_expresion;
        }

        #endregion
  
        //***************************************************************

        //Region que contiene los metodos de la clase 
        #region metodos y funciones 

        //Se encarga de calcular el valor de X y Y
        public void PosicionNodo(ref int xmin, int ymin)
        {
            int aux1, aux2;
            nCoordenadaY = (int)(ymin + nRadio / 2);
            //obtiene la posición del sub-árbol izquierdo
            if (NoIzquierdo != null)
            {
                NoIzquierdo.PosicionNodo(ref xmin, ymin + nRadio + nDistanciaV);
            }
            if ((NoIzquierdo != null) && (NoDerecho != null))
            {
                xmin += nDistanciaH;
            }
            //si existe nodo derecho y el nodo izquierdo deja un espacio entre ellos
            if (NoDerecho != null)
            {
                NoDerecho.PosicionNodo(ref xmin, ymin + nRadio + nDistanciaV);
            }
            if (NoIzquierdo != null && NoDerecho != null)
                nCoordenadaX = (int)((NoIzquierdo.nCoordenadaX + NoDerecho.nCoordenadaX) / 2);
            else
            if (NoIzquierdo != null)
            {
                aux1 = NoIzquierdo.nCoordenadaX;
                NoIzquierdo.nCoordenadaX = nCoordenadaX - 80;
                nCoordenadaX = aux1;
            }
            else if (NoDerecho != null)
            {
                aux2 = NoDerecho.nCoordenadaX;
                //no hay nodo izquierdo,se centra el nodo derecho
                NoDerecho.nCoordenadaX = nCoordenadaX + 80;
                nCoordenadaX = aux2;
            }
            else
            {
                nCoordenadaX = (int)(xmin + nRadio / 2);
                xmin += nRadio;
            }
        }

        //Función para dibujar las ramas de los nodos izquierdo y derecho
        public void DibujarRamas(Graphics grafo, Pen Lapiz)
        {
            if (NoIzquierdo != null)
            // Dibujará rama izquierda
            {
                grafo.DrawLine(Lapiz, nCoordenadaX, nCoordenadaY, NoIzquierdo.nCoordenadaX, NoIzquierdo.nCoordenadaY);
                NoIzquierdo.DibujarRamas(grafo, Lapiz);
            }
            if (NoDerecho != null)
            // Dibujará rama derecha
            {
                grafo.DrawLine(Lapiz, nCoordenadaX, nCoordenadaY,
               NoDerecho.nCoordenadaX, NoDerecho.nCoordenadaY);
                NoDerecho.DibujarRamas(grafo, Lapiz);
            }
        }
        //Función para dibujar el nodo en la posición especificada
        public void DibujarNodo(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, Brush encuentro)
        {
            col = grafo;
            // Dibuja el contorno del nodo
            Rectangle rect = new Rectangle((int)(nCoordenadaX - nRadio / 2), (int)(nCoordenadaY - nRadio / 2), nRadio, nRadio);
            grafo.FillEllipse(encuentro, rect);
            grafo.FillEllipse(Relleno, rect);
            grafo.DrawEllipse(Lapiz, rect);
            grafo.DrawEllipse(Lapiz, rect);
            // Para dibujar el nombre del nodo, es decir el contenido
            //Especifica el formato de las letras 
            StringFormat formato = new StringFormat();
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            //Se encarga de dibujar el valor de las claves 
            grafo.DrawString(this.expresion.ToString(), fuente, RellenoFuente, nCoordenadaX, nCoordenadaY, formato);
            //Dibuja los nodos hijos derecho e izquierdo.
            if (NoIzquierdo != null)
            {
                NoIzquierdo.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, encuentro);
            }
            if (NoDerecho != null)
            {
                NoDerecho.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, encuentro);
            }
        }

        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz)
        {
            //Dibuja el contorno del nodo.
            Rectangle rect = new Rectangle((int)(nCoordenadaX - nRadio / 2), (int)(nCoordenadaY - nRadio / 2), nRadio, nRadio);
            prueba = new Rectangle((int)(nCoordenadaX - nRadio / 2), (int)(nCoordenadaY - nRadio / 2), nRadio, nRadio);
            grafo.FillEllipse(Relleno, rect);
            grafo.DrawEllipse(Lapiz, rect);
            //Dibuja el nombre
            StringFormat formato = new StringFormat();
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(expresion.ToString(), fuente, RellenoFuente, nCoordenadaX, nCoordenadaY, formato);
        }

        #endregion


    }
}
