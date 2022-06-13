using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MASCOSHOP
{
    public partial class BuscarProducto : Form
    {
        public BuscarProducto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string buscar =  textBox1.Text.ToString().Replace(" ","%");
            if (buscar.Length <= 3)
            {
                MessageBox.Show("Introducir datos o mas datos");
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = 6;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 350;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Categoria";
                dataGridView1.Columns[2].HeaderText = "Subcategoria";
                dataGridView1.Columns[3].HeaderText = "Producto";
                dataGridView1.Columns[4].HeaderText = "Precio_venta";
                dataGridView1.Columns[5].HeaderText = "Existencia";
                ConexionDB c = new ConexionDB();
                c.BuscarProductoNombre(dataGridView1, buscar);
            }
        }
    }
}
