using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            try
            {
                string cadenaBuscar = textBox1.Text.ToString().Replace(" ", "%");
                if (cadenaBuscar.Length <= 3)
                {
                    throw new InvalidOperationException("Favor de introducir más datos");
                }

                var gestorProductos = new Business.GestorProductos();
                var listaProductos = gestorProductos.RegresarProductoBuscarPorNombre(cadenaBuscar);

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = 6;
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Categoría";
                dataGridView1.Columns[2].HeaderText = "Subcategoría";
                dataGridView1.Columns[3].HeaderText = "Producto";
                dataGridView1.Columns[4].HeaderText = "Precio Venta";
                dataGridView1.Columns[5].HeaderText = "Existencia";

                foreach (var p in listaProductos)
                {
                    dataGridView1.Rows.Add(p.ID, p.Categoria, p.Subcategoria, p.Producto, p.PrecioVenta, p.Existencia);
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (InvalidOperationException ex)
            {
                MostrarError(ex.Message + " ¡ESTUPID@!");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                // Errores inesperados (BD caídas, etc.)
                MostrarError("Error al procesar la venta:\n" + ex.Message);
                LimpiarCampos();
            }
        }
         private void LimpiarCampos()
        {
            textBox1.Text = "";
        }
        private static void MostrarError(string msg)
        {
            MessageBox.Show(msg);
        }

    }
}
