using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MASCOSHOP
{
    public partial class AgregarAjuste : Form
    {
        public AgregarAjuste()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ValidarFormatos.ValidarCampoDecimal(textBox2);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ValidarFormatos.ValidarCampoDecimal(textBox3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0)
            {
                MessageBox.Show("INTRODUCE LOS DATOS, ¡ESTUPID@!");
            }
            else
            {
                ConexionDB c = new ConexionDB();
                Ajustes Ajs = new Ajustes()
                {
                    Descripcion = textBox1.Text.ToString(),
                    Precio_venta = Convert.ToDecimal(textBox2.Text),
                    Precio_ganancia = Convert.ToDecimal(textBox3.Text),
                    Fecha = DateTime.Today
                };
                if (c.InserAjuste(Ajs))
                {
                    MessageBox.Show("Ajuste insertado correctamente");
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Descripcion";
            dataGridView1.Columns[2].HeaderText = "Precio_Venta";
            dataGridView1.Columns[3].HeaderText = "Precio_Ganancia";
            dataGridView1.Columns[4].HeaderText = "Fecha";
            ConexionDB c = new ConexionDB();
            c.VerAjustes(dataGridView1);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
