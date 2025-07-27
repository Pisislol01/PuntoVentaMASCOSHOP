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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            try
            {
                var producto = new Productos();
                var precios = new Precios();
                ValidarString(comboBox1.Text);
                producto.Categoria = comboBox1.Text;
                ValidarString(comboBox2.Text);
                producto.Subcategoria = comboBox2.Text;
                ValidarString(textBox3.Text);
                producto.Producto = textBox3.Text;
                var numeroProductosComprados = ValidarInt(textBox4.Text);
                precios.PrecioCompra = ValidarDecimal(textBox5.Text);
                precios.PrecioVenta = ValidarDecimal(textBox6.Text);

                var gestorProductos = new GestorProductos();
                producto.ID = gestorProductos.ObtenerIdNuevoProducto();
                precios.ID = producto.ID;

                gestorProductos.AgregarProductoNuevo(producto);

                var gestorInventario = new GestorInventario();
                gestorInventario.AgregarInventarioNuevo(producto.ID, numeroProductosComprados);

                var gestorPrecios = new GestorPrecios();
                gestorPrecios.AgregarPrecioNuevo(precios);

                MessageBox.Show("Producto Agregado exitosamente, El nuevo ID es: " + producto.ID);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + " ¡ESTUPID@!");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la venta:\n" + ex.Message);
                LimpiarCampos();
            }
            finally
            {
                LimpiarCampos();
            }
        }
    


        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void AgregarProductoNuevo_Load(object sender, EventArgs e)
        {
            try
            {
                var gestorProductos = new Business.GestorProductos();
                var categorias = gestorProductos.ObtenerCategorias();
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(categorias.ToArray());
                var subCategorias = gestorProductos.BuscarSubCategoria();
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(subCategorias.ToArray());
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + " ¡ESTUPID@!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la venta:\n" + ex.Message);
            }
        }
        private static decimal ValidarDecimal(string stringDecimalValidar)
        {
            if (stringDecimalValidar.IsNullOrEmpty())
            {
                throw new InvalidOperationException(
                    $"El decimal={stringDecimalValidar} esta vacia");
            }
            if (!decimal.TryParse(stringDecimalValidar, out decimal decimalValidar))
            {
                throw new InvalidOperationException(
                    $"El decimal={stringDecimalValidar} es incorrecta");
            }
            if (decimalValidar <= 0)
            {
                throw new InvalidOperationException(
                    $"El decimal={stringDecimalValidar} debe ser mayor a ceros");
            }
            return decimalValidar;

        }
        private static int ValidarInt(string stringIntValidar)
        {
            if (stringIntValidar.IsNullOrEmpty())
            {
                throw new InvalidOperationException(
                    $"El entero={stringIntValidar} esta vacia");
            }
            if (!int.TryParse(stringIntValidar, out int intValidar))
            {
                throw new InvalidOperationException(
                    $"El entero={stringIntValidar} es incorrecto");
            }
            if (intValidar <= 0)
            {
                throw new InvalidOperationException(
                    $"El entero={stringIntValidar} debe ser mayor a ceros");
            }
            return intValidar;
        }
        private static void ValidarString(string stringValidar)
        {
            if (stringValidar.IsNullOrEmpty())
            {
                throw new InvalidOperationException(
                    $"El campo={stringValidar} esta vacio");
            }
        }
        private void LimpiarCampos()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
    }
}
