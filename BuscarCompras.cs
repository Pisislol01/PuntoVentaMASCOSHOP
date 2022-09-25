using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MASCOSHOP
{
    public partial class BuscarCompras : Form
    {
        public BuscarCompras()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Fecha = textBox1.Text.ToString();
            try
            {
                DateTime FechaValida = DateTime.Parse(Fecha);
                if(FechaValida.ToString("yyyy-MM-dd") == Fecha)
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.ColumnCount = 5;
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Producto";
                    dataGridView1.Columns[2].HeaderText = "Cantidad";
                    dataGridView1.Columns[3].HeaderText = "Precio";
                    dataGridView1.Columns[4].HeaderText = "Fecha";
                    ConexionDB c = new ConexionDB();
                    c.BuscarCompras(Fecha,dataGridView1);
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                else
                {
                    MessageBox.Show("Formato invalido (yyyy-MM-dd): " + Fecha);
                }                
            }
            catch
            {
                MessageBox.Show("Escriba una fecha: " + Fecha);
            }
        }

        private void BuscarCompras_Load(object sender, EventArgs e)
        {
            DateTime Fecha = DateTime.Today;
            string FechaF = Fecha.ToString("yyyy-MM-dd");
            textBox1.Text = FechaF;
        }
    }
}
