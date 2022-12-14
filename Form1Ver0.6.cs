using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ArbolDe_Expresiones
{
    public partial class FormArbol : Form
    {
        //conectamos a la clase ConexionBD
        ConexionBD con = new ConexionBD();

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
            txtInsertar.Clear();
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
            /*Random num = new Random();
            int idExpresion = num.Next(1, 999);*/

            //capturamos la informacion del textbox
            string expresion = txtInsertar.Text;

            ColorCambioButton();
            btnGuardarBD.BackColor = Color.FromArgb(225, 100, 40);
            PanelGrafico.Visible = false;

            //abrimos la conexion
            con.IniciarConexion();

            //comando que permitira guardar la informacion en la base de datos 
            SqlCommand comando = new SqlCommand("INSERT INTO Lista_Expresiones VALUES('" + expresion + "')", con.conectarBD); 


            //Al ejecutarse el comando le enviara a la variable r un numero (0 o1) el cual nos dira si funciono o no
            //0 = no 1= si
            int r = comando.ExecuteNonQuery();
            if (r > 0 )
            {
                MessageBox.Show("Datos guardados correctamente");
            }
            else
            {
                MessageBox.Show("Error al ingresar datos","Error"+MessageBoxIcon.Error);
            }
            //cerramos la conexion
            con.TerminarConexion();
        }

        private void btnMostrarE_Click(object sender, EventArgs e)
        {
            subMenuExpresiones.Visible = true;
            ColorCambioButton();
            btnMostrarE.BackColor = Color.FromArgb(225, 100, 40);
            if (txtInsertar.Text != "")
            {
      
                Refresh();
            }
            else
            {
                MessageBox.Show("No existe expresion que mostrar");
            }

            PanelGrafico.Visible = false;
        }

        private void btnInorden_Click(object sender, EventArgs e)
        {
            btnInorden.BackColor = Color.FromArgb(225, 100, 40);
            
            lbl_expresion.Text = arbol.InsertarIn(Raiz);
            lbl_expresion.Visible = true;

            if (lbl_textinorden.Equals(lbl_textinorden))
            {
                lbl_textinorden.Visible = true;
                lbl_textposorden.Visible = false;
                lbl_textpreorden.Visible = false;
                lbl_textexpresion.Visible = false;
            }

            PanelGrafico.Visible = false;
            subMenuExpresiones.Visible = false;
            arbol.Limpiar();
        }

        private void btnPosorden_Click(object sender, EventArgs e)
        {
            btnPosorden.BackColor = Color.FromArgb(225, 100, 40);

            lbl_expresion.Text = arbol.InsertarPos(Raiz);
            lbl_expresion.Visible = true;

            if (lbl_textposorden.Equals(lbl_textposorden))
            {
                lbl_textinorden.Visible = false;
                lbl_textposorden.Visible = true;
                lbl_textpreorden.Visible = false;
                lbl_textexpresion.Visible = false;
            }

            subMenuExpresiones.Visible = false;
            PanelGrafico.Visible = false;

            arbol.Limpiar();
        }

        private void btnPreorden_Click(object sender, EventArgs e)
        {
            btnPreorden.BackColor = Color.FromArgb(225, 100, 40);

            lbl_expresion.Text = arbol.InsertarPre(Raiz);
            lbl_expresion.Visible = true;

            if (lbl_textpreorden.Equals(lbl_textpreorden))
            {
                lbl_textinorden.Visible = false;
                lbl_textposorden.Visible = false;
                lbl_textpreorden.Visible = true;
                lbl_textexpresion.Visible = false;
            }

            subMenuExpresiones.Visible = false;
            PanelGrafico.Visible = false;

            arbol.Limpiar();
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

        private void btnBuscarS_Click(object sender, EventArgs e)
        {
            //Abrimos la conexion 
            con.IniciarConexion();

            //Hacemos la comnsulta 
            string consulta = "select * from Lista_Expresiones where idExpresiones=" + txtInsertar.Text + "";
            
            SqlCommand comando= new SqlCommand(consulta,con.conectarBD);
            //Para leer los datos hacemos uso del SqlDataReader
            SqlDataReader lector; 
            //Usamos un if para verificar si hay datos que puedan ser leidos 
            lector = comando.ExecuteReader();

            if (lector.Read())
            {
                txtRes.Text = lector["Expresion"].ToString();
            }
            else
            {
                MessageBox.Show("La expresion buscada no existe");
            }
            txtInsertar.Clear();
            con.TerminarConexion();
        }
    }
}
