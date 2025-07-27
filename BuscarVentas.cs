using MASCOSHOP.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MASCOSHOP
{
    public partial class BuscarVentas : Form
    {
        public BuscarVentas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime Fecha = ValidarFecha(textBox1.Text);

                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                dataGridView1.ColumnCount = 6;

                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Producto";
                dataGridView1.Columns[2].HeaderText = "Cantidad";
                dataGridView1.Columns[3].HeaderText = "Precio";
                dataGridView1.Columns[4].HeaderText = "Ganancia";
                dataGridView1.Columns[5].HeaderText = "Fecha";

                var gestoVentas = new GestorVentas();
                var totalVentas = gestoVentas.ObtenerVentasPorFecha2(Fecha);

                foreach (var venta in totalVentas)
                {
                    dataGridView1.Rows.Add(
                        venta.ID,
                        venta.Producto,
                        venta.Cantidad,
                        venta.Precio,
                        venta.Ganancia,
                        venta.Fecha.ToString("yyyy-MM-dd"));
                }
                decimal totalImporte = totalVentas.Sum(v => v.Precio);
                decimal totalGanancia = totalVentas.Sum(v => v.Ganancia);
                decimal totalRegistros = totalVentas.Sum(v => v.Cantidad);
                dataGridView1.Rows.Add(
                    "", 
                    "TOTAL:",
                    totalRegistros,
                    totalImporte,
                    totalGanancia,
                    "" 
                    );
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + " ¡ESTUPID@!");
                textBox1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar las Ventas" + ex.Message);
                textBox1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
            finally
            {

            }
        }

        private void BuscarCompras_Load(object sender, EventArgs e)
        {
            DateTime Fecha = DateTime.Today;
            string FechaF = Fecha.ToString("yyyy-MM-dd");
            textBox1.Text = FechaF;
        }
        private static DateTime ValidarFecha(string fechaTexto)
        {
            if (!DateTime.TryParseExact(fechaTexto, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
            {
                throw new InvalidOperationException("Fecha inválida. Usa el formato yyyy-MM-dd.");
            }
            return fecha;
        }
    }
}
