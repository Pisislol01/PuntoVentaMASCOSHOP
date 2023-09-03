using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MASCOSHOP
{
    public partial class AgregarCompras : Form
    {
        public AgregarCompras()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ValidarFormatos.ValidarCampoDecimal(textBox2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                MessageBox.Show("INTRODUCE LOS DATOS, ¡ESTUPID@!");
            }
            else
            {
                ConexionDB c = new ConexionDB();
                Compras Cpr = new Compras()
                {
                    ID = c.SelectIdMaxCompras() + 1,
                    Descripcion = textBox1.Text.ToString(),
                    Precio = Convert.ToDecimal(textBox2.Text),
                    Fecha = DateTime.Today
                };
                if (c.InsertCompra(Cpr))
                {
                    MessageBox.Show("Compra insertada con exito");
                }
                textBox1.Text = "";
                textBox2.Text = "";
            }             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Descripcion";
            dataGridView1.Columns[2].HeaderText = "Precio";
            dataGridView1.Columns[3].HeaderText = "Fecha";
            ConexionDB c = new ConexionDB();
            c.VerCompras(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
