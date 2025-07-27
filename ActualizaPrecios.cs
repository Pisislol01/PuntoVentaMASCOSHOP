using MASCOSHOP.Business;
using MASCOSHOP.DTO;
using Microsoft.IdentityModel.Tokens;
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

            try
            {
                var idProducto = ValidarInt(textBox1);
                var gestorProductos = new GestorProductos();
                var productos = gestorProductos.ObtenerProductoID(idProducto);
                textBox2.Text = productos.Producto;
                var gestorPrecios = new GestorPrecios();
                var precios = gestorPrecios.ObtenerPrecios(idProducto);
                textBox3.Text = precios.PrecioCompra.ToString();
                textBox4.Text = precios.PrecioVenta.ToString();
            }
            catch (InvalidOperationException ex)
            {
                textBox2.Text =  "Error: " + ex.Message + " ¡ESTUPID@!";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " ¡ESTUPID@!");
            }
            finally
            {
                if(textBox1.Text.Length == 0)
                {
                    LimpiarCampos();
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Precios precios = new Precios()
                {
                    ID = ValidarInt(textBox1),
                    PrecioCompra = ValidarDecimal(textBox5),
                    PrecioVenta = ValidarDecimal(textBox6)
                };

                var gestorPrecios = new GestorPrecios();
                gestorPrecios.ActualizarPreciosID(precios);

                MessageBox.Show("Actualizacion exitosa");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error: " + ex.Message + " ¡ESTUPID@!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " ¡ESTUPID@!");
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
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
        private int ValidarInt(TextBox tb)
        {
            if (tb.Text.IsNullOrEmpty())
            {
                throw new InvalidOperationException(
                    $"El ID={tb.Text} esta vacio");
            }
            if (!int.TryParse(textBox1.Text, out int intValidar))
            {
                throw new InvalidOperationException(
                    $"El ID={tb.Text} es incorrecto");
            }
            return intValidar;
        }
        private decimal ValidarDecimal(TextBox tb)
        {
            if (tb.Text.IsNullOrEmpty())
            {
                throw new InvalidOperationException(
                    $"La Cantidad={tb.Text} esta vacia");
            }
            if (!decimal.TryParse(tb.Text, out decimal decimalValidar))
            {
                throw new InvalidOperationException(
                    $"La Cantidad={tb.Text} es incorrecta");
            }
            if (decimalValidar <= 0)
            {
                throw new InvalidOperationException(
                    $"La Cantidad={tb.Text} debe ser mayor a ceros");
            }
            return decimalValidar;

        }
    }
}
