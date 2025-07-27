using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MASCOSHOP.DTO;
using System.Collections.Generic;
using MASCOSHOP.Business;
using Microsoft.IdentityModel.Tokens;
using MASCOSHOP.Enums;

namespace MASCOSHOP
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InformarProducto();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var intValidar = ValidarIdProducto();
                var decimalValidar = ValidarCantidad();
                if (RBCantidad.Checked == true)
                {
                    ProcesarVenta(intValidar, decimalValidar);
                }
                else if (RBPrecio.Checked == true)
                {
                    ProcesarVentaPrecio(intValidar, decimalValidar);
                }
                else if (RBAgregarProducto.Checked == true)
                {
                    AgregarProducto(intValidar, decimalValidar);
                }
                else if (RBCancelarVenta.Checked == true)
                {
                    CancelarVenta(intValidar, decimalValidar);
                }
            }
            catch (InvalidOperationException ex)
            {
                MostrarError(ex.Message + " ¡ESTUPID@!");
            }
            catch (Exception ex)
            {
                // Errores inesperados (BD caídas, etc.)
                MostrarError("Error al procesar la venta:\n" + ex.Message);
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
                var gestorVentas = new GestorVentas();
                var ventas = gestorVentas.ObtenerVentasPorFecha(DateTime.Today);
                textBox4.Text = ventas.Precio.ToString("C");
                var gestorAjustes = new GestorAjustes();
                var ajustes = gestorAjustes.ObtenerAjustesPorFecha(DateTime.Today);
                textBox6.Text = gestorVentas.ObtenerGananciaTotal(ventas, ajustes).ToString("C");
                var gestorCompras = new GestorCompras();
                var totalCompras = gestorCompras.ObtenerTotalCompras();
                var totalVentas = gestorVentas.ObtenerTotalVentas();
                var totalAjustes = gestorAjustes.ObtenerTotalAjustes();
                textBox5.Text = gestorVentas.ObtenerEfectivoReal(totalVentas, totalCompras, totalAjustes).ToString("C");

            }
            catch (InvalidOperationException ex)
            {
                MostrarError(ex.Message + " ¡ESTUPID@!");
            }
            catch (Exception ex)
            {
                // Errores inesperados (BD caídas, etc.)
                MostrarError("Error al procesar la venta:\n" + ex.Message);
            }
            finally
            {
                LimpiarCamposVenta();
            }
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
        }
        private void CancelarVenta(int idProducto, decimal cantidad)
        {
            var gv = new GestorVentas();
            var ventasCancelar = gv.ObtenerTotalVentasCancelar(idProducto, cantidad);
            MessageBox.Show($"Se cancelaran en total {ventasCancelar.ID} ventas " +
                $"cantidad {ventasCancelar.Cantidad} " +
                $"precio {ventasCancelar.Precio} " +
                $"ganancia {ventasCancelar.Ganancia}");
            gv.EliminarVentasPorIdCantidadFecha(idProducto, cantidad);
            var gi = new GestorInventario();
            gi.ActualizarInventarioCancelarVenta(idProducto, ventasCancelar.Cantidad);
            MessageBox.Show("Ventas eliminadas con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void AgregarProducto(int idProducto, decimal cantidad)
        {
            var gi = new GestorInventario();
            gi.AgregarProductoInventario(idProducto, cantidad);
            MessageBox.Show("Se agrego el producto de manera exitosa.");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            AgregarProductoNuevo frm = new AgregarProductoNuevo();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BuscarProducto frm = new BuscarProducto();
            frm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ActualizaPrecios frm = new ActualizaPrecios();
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AgregarCompras frm = new AgregarCompras();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AgregarAjuste frm = new AgregarAjuste();
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            BuscarVentas frm = new BuscarVentas();
            frm.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            InformarProducto();
        }
        private void InformarProducto()
        {
            try
            {
                int idProducto = 1;
                decimal cantidadOPrecio = 1;
                if (textBox1.Text != "")
                {
                    idProducto = ValidarIdProducto();
                }
                if (textBox3.Text != "")
                {
                    cantidadOPrecio = ValidarCantidad2();
                }
                var gp = new GestorProductos();
                var producto = gp.ObtenerProductoID(idProducto);
                var gpr = new GestorPrecios();
                var precio = gpr.ObtenerPrecios(idProducto);

                var modo = ObtenerModoDeVenta();

                decimal precioVenta = gpr.CalcularPrecio(precio.PrecioVenta, cantidadOPrecio, modo);
                textBox2.Text = producto.Producto + " - " + precioVenta.ToString("C");
            }
            catch (InvalidOperationException ex)
            {
                textBox2.Text = ex.Message + " ¡ESTUPID@!";
            }
            catch (Exception ex)
            {
                // Errores inesperados (BD caídas, etc.)
                MostrarError("Error al procesar la venta:\n" + ex.Message);
                LimpiarCampos();
            }
        }
        private ModoVenta ObtenerModoDeVenta()
        {
            if (RBCantidad.Checked) return ModoVenta.Cantidad;
            if (RBPrecio.Checked) return ModoVenta.Precio;
            return ModoVenta.Otro;
        }

        private void RBCantidad_CheckedChanged(object sender, EventArgs e)
        {
            InformarProducto();
        }

        private void RBPrecio_CheckedChanged(object sender, EventArgs e)
        {
            InformarProducto();
        }

        private void RBAgregarProducto_CheckedChanged(object sender, EventArgs e)
        {
            InformarProducto();
        }

        private void RBCancelarVenta_CheckedChanged(object sender, EventArgs e)
        {
            InformarProducto();
        }
        private void bAgregarVentas_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Archivos CSV (*.csv)|*.csv";
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                var gestorLotes = new GestorLotes();
                gestorLotes.ProcesarArchivoEntrada(ofd);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo: " + ex.Message);
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
            RBCantidad.Checked = true;
        }
        private void LimpiarCamposVenta()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            RBCantidad.Checked = true;
        }
        private void MostrarError(string msg)
        {
            MessageBox.Show(msg);
        }
        private int ValidarIdProducto()
        {
            if (textBox1.Text.IsNullOrEmpty())
            {
                throw new InvalidOperationException(
                    $"El ID={textBox1.Text} esta vacio");
            }
            if (!int.TryParse(textBox1.Text, out int intValidar))
            {
                throw new InvalidOperationException(
                    $"El ID={textBox1.Text} es incorrecto");
            }
            return intValidar;
        }
        private decimal ValidarCantidad()
        {
            if (textBox3.Text.IsNullOrEmpty())
            {
                throw new InvalidOperationException(
                    $"La Cantidad={textBox3.Text} esta vacia");
            }
            if (!decimal.TryParse(textBox3.Text, out decimal decimalValidar))
            {
                throw new InvalidOperationException(
                    $"La Cantidad={textBox3.Text} es incorrecta");
            }
            if (decimalValidar <= 0)
            {
                throw new InvalidOperationException(
                    $"La Cantidad={textBox3.Text} debe ser mayor a ceros");
            }
            return decimalValidar;

        }
        private decimal ValidarCantidad2()
        {
            if (textBox3.Text.IsNullOrEmpty())
            {
                throw new InvalidOperationException(
                    $"La Cantidad={textBox3.Text} esta vacia");
            }
            if (!decimal.TryParse(textBox3.Text, out decimal decimalValidar))
            {
                throw new InvalidOperationException(
                    $"La Cantidad={textBox3.Text} es incorrecta");
            }
            return decimalValidar;
        }
        private void ProcesarVenta(int idProducto, decimal cantidad)
        {
            var gestorProd = new GestorProductos();
            gestorProd.VerificarExistenciaProducto(idProducto);

            var gestorInv = new GestorInventario();
            var inventario = gestorInv.AsegurarInventarioDisponible(idProducto, cantidad);
            
            var gestorPre = new GestorPrecios();
            var precios = gestorPre.ObtenerPrecios(idProducto);

            var gestorVen = new GestorVentas();
            gestorVen.CrearVenta(precios, cantidad);

            gestorInv.ActualizarInventarioVenta(inventario, cantidad);

            MessageBox.Show("Venta registrada con éxito.");
        }
        private void ProcesarVentaPrecio(int idProducto, decimal precio)
        {            
            var gestorPre = new GestorPrecios();
            var precios = gestorPre.ObtenerPrecios(idProducto);            
            decimal cantidad = gestorPre.CalcularCantidadDesdePrecio(precio, precios.PrecioVenta);
            ProcesarVenta(idProducto, cantidad);
        }
    }
}
