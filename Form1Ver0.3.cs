using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArbolDe_Expresiones
{
    public partial class FormArbol : Form
    {

        //Propiedades a emplear
        private Nodo Raiz;
        Arbol arbol = new Arbol();
        Graphics g;
        //Variable para almacenar y mostrar la expresion ingresada
        public string datos; 


        public FormArbol()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Este metodo se encarga de regresar los botones a su color original 
        private void ColorCambioButton()
        {
            btnInsertarE.BackColor = Color.FromArgb(54, 51, 51);
            btnGrafico.BackColor = Color.FromArgb(54, 51, 51);
            btnGuardarBD.BackColor = Color.FromArgb(54, 51, 51);
            btnMostrarE.BackColor = Color.FromArgb(54, 51, 51);
            btnInorden.BackColor = Color.FromArgb(54, 51, 51);
            btnPosorden.BackColor = Color.FromArgb(54, 51, 51);
            btnPreorden.BackColor = Color.FromArgb(54, 51, 51);
        }

        private void btnInsertarE_Click(object sender, EventArgs e)
        {
            ColorCambioButton();
            btnInsertarE.BackColor = Color.FromArgb(225, 100, 40);
            if (txtInsertar.Text != "")
            {
                arbol.Insertar(txtInsertar.Text);
                datos = txtInsertar.Text;
                PanelGrafico.Visible = false;

                MessageBox.Show("Expresion insertada correctamente\n" + datos);
            }
            else
            {
                MessageBox.Show("Debe ingresar la expresion requeridad");
            }
        }

        private void btnGrafico_Click(object sender, EventArgs e)
        {
                     
            ColorCambioButton();
            btnGrafico.BackColor = Color.FromArgb(225, 100, 40);
            if (txtInsertar.Text != "")
            {
                Raiz = arbol.CrearArbol();
                PanelGrafico.Visible = true;
                Refresh();
                Refresh();
            }
            else
            {
                MessageBox.Show("No hay un grafico que mostrar");
            }
        }

        private void btnGuardarBD_Click(object sender, EventArgs e)
        {
            ColorCambioButton();
            btnGuardarBD.BackColor = Color.FromArgb(225, 100, 40);
            PanelGrafico.Visible = false;
        }

        private void btnMostrarE_Click(object sender, EventArgs e)
        {
            subMenuExpresiones.Visible = true;
            ColorCambioButton();
            btnMostrarE.BackColor = Color.FromArgb(225, 100, 40);
            PanelGrafico.Visible = false;
        }

        private void btnInorden_Click(object sender, EventArgs e)
        {
            btnInorden.BackColor = Color.FromArgb(225, 100, 40);
            subMenuExpresiones.Visible = false;
            PanelGrafico.Visible = false;
        }

        private void btnPosorden_Click(object sender, EventArgs e)
        {
            btnPosorden.BackColor = Color.FromArgb(225, 100, 40);
            subMenuExpresiones.Visible = false;
            PanelGrafico.Visible = false;
        }

        private void btnPreorden_Click(object sender, EventArgs e)
        {
            btnPreorden.BackColor = Color.FromArgb(225, 100, 40);
            subMenuExpresiones.Visible = false;
            PanelGrafico.Visible = false;
        }

        private void PanelGrafico_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;//para ajustar el texto
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//Calidad de los graficos
            g = e.Graphics;
            //Creamos un objeto de la clase Font 
            Font fuente = new Font("Bookman Old Style", 15);//Asignamos la fuente y el tamaño
            arbol.DibujarArbol(g, fuente, Brushes.Pink, Brushes.Blue, Pens.Black, Brushes.Blue);
        }
    }
}
