using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MASCOSHOP.DTO;

namespace MASCOSHOP
{
    public partial class AgregarProductoNuevo : Form
    {
        public AgregarProductoNuevo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length < 1 || comboBox2.Text.Length < 1 || textBox3.Text.Length < 1 ||
                textBox4.Text.Length < 1 || textBox5.Text.Length < 1 || textBox6.Text.Length < 1)
            {
                MessageBox.Show("INTRODUCE TODOS LOS DATOS, ¡ESTUPID@!");
            }
            else
            {
                ConexionDB c = new ConexionDB();
                Productos pdt = new Productos()
                {
                    ID = c.SelectIdMaxProductos() + 1,
                    Categoria = comboBox1.Text.ToString(),
                    Subcategoria = comboBox2.Text.ToString(),
                    Producto = textBox3.Text.ToString()
                };
                if(c.InsertProducto(pdt)){
                    Inventario Inv = new Inventario()
                    {
                        ID = pdt.ID,
                        CompraIni = 0,
                        Compra = Convert.ToDecimal(textBox4.Text.ToString()),
                        Venta = 0,
                        Existencia = Convert.ToDecimal(textBox4.Text.ToString())
                    };
                    if (c.InsertInventario(Inv))
                    {
                        Precios Prc = new Precios()
                        {
                            ID = pdt.ID,
                            PrecioCompra = Convert.ToDecimal(textBox5.Text.ToString()),
                            PrecioVenta = Convert.ToDecimal(textBox6.Text.ToString())
                        };
                        if (c.InsertPrecios(Prc))
                        {
                            MessageBox.Show("Producto Agregado exitosamente, El nuevo ID es: " + pdt.ID);
                        }
                    }
                }
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ValidarFormatos.ValidarCampoDecimal(textBox4);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            ValidarFormatos.ValidarCampoDecimal(textBox5);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            ValidarFormatos.ValidarCampoDecimal(textBox6);
        }

        private void AgregarProductoNuevo_Load(object sender, EventArgs e)
        {
            ConexionDB c = new ConexionDB();
            c.BuscarCategorias(comboBox1);
            c.BuscarSubCategoria(comboBox2);
        }
    }
}
