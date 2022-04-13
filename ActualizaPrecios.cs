using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MASCOSHOP
{
    public partial class ActualizaPrecios : Form
    {
        public ActualizaPrecios()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ValidarFormatos.ValidarCampoInt(textBox1) == true)
            {
                if (textBox1.Text.Length > 0)
                {
                    ConexionDB c = new ConexionDB();
                    int ID = Convert.ToInt32(textBox1.Text);
                    Productos Prd1 = new Productos()
                    {
                        ID = ID
                    };
                    c.SelectProductoID(Prd1);
                    textBox2.Text = Prd1.Producto;
                    Precios Prc = new Precios()
                    {
                        ID = ID
                    };
                    c.SelectPreciosID(Prc);
                    textBox3.Text = Prc.PrecioCompra.ToString();
                    textBox4.Text = Prc.PrecioVenta.ToString();
                }
            }
            if (textBox1.Text.Length == 0)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            ValidarFormatos.ValidarCampoDecimal(textBox6);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            ValidarFormatos.ValidarCampoDecimal(textBox5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 0 || textBox5.Text.Length == 0 || textBox6.Text.Length == 0)
            {
                MessageBox.Show("INTRODUCE TODOS LOS DATOS, ¡ESTUPID@!");
            }
            else
            {
                ConexionDB c = new ConexionDB();
                Precios Prc = new Precios()
                {
                    ID = Convert.ToInt32(textBox1.Text.ToString()),
                    PrecioCompra = Convert.ToDecimal(textBox5.Text.ToString()),
                    PrecioVenta = Convert.ToDecimal(textBox6.Text.ToString())
                };
                if (c.UpdatePrecios(Prc))
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    MessageBox.Show("Actualizacion exitosa");
                }
            }
        }
    }
}
