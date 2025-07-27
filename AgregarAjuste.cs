using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MASCOSHOP.Business;
using MASCOSHOP.DTO;
using Microsoft.IdentityModel.Tokens;

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
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarString(textBox1.Text);
                decimal precioVenta = ValidarDecimal(textBox2.Text);
                decimal precioGanancia = ValidarDecimal(textBox3.Text);
                var agregarAjuste = new GestorAjustes();
                agregarAjuste.AgregarAjuste(textBox1.Text, precioVenta, precioGanancia);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + " ¡ESTUPID@!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el Ajuste: " + ex.Message);
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

                dataGridView1.ColumnCount = 5;
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Descripcion";
                dataGridView1.Columns[2].HeaderText = "Precio de Venta";
                dataGridView1.Columns[3].HeaderText = "Precio de Ganancia";
                dataGridView1.Columns[4].HeaderText = "Fecha";

                var gestorAjustes = new GestorAjustes();
                var totalAjustes = gestorAjustes.ObtenerAjustes();

                foreach(var ajustes in totalAjustes)
                {
                    dataGridView1.Rows.Add(
                        ajustes.ID,
                        ajustes.Descripcion,
                        ajustes.Precio_venta,
                        ajustes.Precio_ganancia,
                        ajustes.Fecha.ToString("yyyy-MM-dd")
                        );
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + " ¡ESTUPID@!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al mostrar los Ajustes: " + ex.Message);
            }
            finally
            {
                LimpiarCampos();
            }
        }    
    private void LimpiarCampos()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private static void ValidarString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new InvalidOperationException("La descripcion no puede estar vacía.");
            }
        }
        private static decimal ValidarDecimal(string input)
        {
            if(input.IsNullOrEmpty())
            {
                throw new InvalidOperationException("El o los precios deben de informarse.");
            }
            if (decimal.TryParse(input, out decimal result))
            {
                return result;
            }
            throw new InvalidOperationException("El o los precios deben de ser numericos.");
        }
    } 
}
