using MASCOSHOP.Business;
using MASCOSHOP.DTO;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var descripcion = ValidarString(textBox1.Text);
                var importe = Validardecimal(textBox2.Text);
                var gesorcompras = new GestorCompras();
                gesorcompras.AgregarNuevaCompra(descripcion, importe);
                MessageBox.Show("Compra insertada con exito");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + " ¡ESTUPID@!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message + " ¡ESTUPID@!");
            }
            finally
            {
                LimpiarCampos();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                dataGridView1.ColumnCount = 4;
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Descripción";
                dataGridView1.Columns[2].HeaderText = "Precio";
                dataGridView1.Columns[3].HeaderText = "Fecha";

                var gestorCompras = new GestorCompras();
                var compras = gestorCompras.ObtenerCompras();

                foreach (var compra in compras)
                {
                    dataGridView1.Rows.Add(
                        compra.ID,
                        compra.Descripcion,
                        compra.Precio,
                        compra.Fecha.ToString("yyyy-MM-dd") // <- opcional
                    );
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message + " ¡ESTUPID@!");
            }
            finally
            {
                LimpiarCampos();
            }
        }
        private decimal Validardecimal(string cantidadString)
        {
            if (string.IsNullOrEmpty(cantidadString))
            {
                throw new InvalidOperationException(
                    $"La Cantidad está vacía.");
            }

            if (!decimal.TryParse(cantidadString, out decimal decimalValidar))
            {
                throw new InvalidOperationException(
                    $"La Cantidad '{cantidadString}' no es un número válido.");
            }

            if (decimalValidar <= 0)
            {
                throw new InvalidOperationException(
                    $"La Cantidad '{cantidadString}' debe ser mayor a cero.");
            }

            return decimalValidar;
        }
        private string ValidarString(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
            {
                throw new InvalidOperationException(
                    $"La Descripción está vacía.");
            }
            if (cadena.Length > 100)
            {
                throw new InvalidOperationException(
                    $"La Descripción '{cadena}' no puede tener más de 50 caracteres.");
            }
            return cadena;
        }
        private void LimpiarCampos()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
