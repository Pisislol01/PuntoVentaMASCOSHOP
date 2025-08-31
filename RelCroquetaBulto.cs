using MASCOSHOP.Business;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MASCOSHOP
{
    public partial class RelCroquetasBulto : Form
    {
        public RelCroquetasBulto()
        {
            InitializeComponent();
        }

        private void bAgregarRelacion_Click(object sender, EventArgs e)
        {
            try
            {
                var gestorRelacionCroquetaBulto = new GestorRelacionCroquetaBulto();
                var gestorProductos = new GestorProductos();

                int iDCroqueta = ValidarInt(tbIDCroqueta.Text);
                gestorProductos.VerificarExistenciaProducto(iDCroqueta);
                int idBulto = ValidarInt(tbIDBulto.Text);
                gestorProductos.VerificarExistenciaProducto(idBulto);
                decimal kilosBulto = ValidarDecimal(tbKilosBulto.Text);

                gestorRelacionCroquetaBulto.AgregarRelacionCroquetasBulto(iDCroqueta, idBulto, kilosBulto);

                MessageBox.Show("Relación de croquetas vs bulto agregada exitosamente.");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + " ¡ESTUPID@!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la relación de croquetas" +
                    " vs bulto: " + ex.Message);
            }
            finally
            {
                LimpiarCampos();
            }
        }
        private void LimpiarCampos()
        {
            tbIDBulto.Text = string.Empty;
            tbIDCroqueta.Text = string.Empty;
            tbKilosBulto.Text = string.Empty;
        }
        private static int ValidarInt(string validarInt)
        {
            if (validarInt.IsNullOrEmpty())
            {
                throw new InvalidOperationException("El valor ingresado no puede estar vacío.");
            }
            if (!int.TryParse(validarInt, out int resultado))
            {
                throw new InvalidOperationException("El valor ingresado no es un número entero válido.");
            }
            if (resultado <= 0)
            {
                throw new InvalidOperationException("El valor ingresado debe ser un número entero positivo.");
            }
            return resultado;
        }
        private static decimal ValidarDecimal(string validarDecimal)
        {
            if (validarDecimal.IsNullOrEmpty())
            {
                throw new InvalidOperationException("El valor ingresado no puede estar vacío.");
            }
            if (!decimal.TryParse(validarDecimal, out decimal resultado))
            {
                throw new InvalidOperationException("El valor ingresado no es un número decimal válido.");
            }
            if (resultado <= 0)
            {
                throw new InvalidOperationException("El valor ingresado debe ser un número decimal positivo.");
            }
            return resultado;
        }

        private void tbIDCroqueta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = string.Empty;
                int idCroqueta = ValidarInt(tbIDCroqueta.Text);
                var gestorProductos = new GestorProductos();
                var producto = gestorProductos.ObtenerProductoID(idCroqueta);
                textBox1.Text = producto.Producto;
            }
            catch (InvalidOperationException ex)
            {
                textBox1.Text = ex.Message;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar el ID de la croqueta: " + ex.Message);
            }

        }

        private void tbIDBulto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = string.Empty;
                int idBulto = ValidarInt(tbIDBulto.Text);
                var gestorProductos = new GestorProductos();
                var producto = gestorProductos.ObtenerProductoID(idBulto);
                textBox2.Text = producto.Producto;
            }
            catch (InvalidOperationException ex)
            {
                textBox2.Text = ex.Message;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar el ID de la croqueta: " + ex.Message);
            }
        }

        private void bVerRelacion_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                dataGridView1.ColumnCount = 5;
                dataGridView1.Columns[0].HeaderText = "ID Croqueta";
                dataGridView1.Columns[1].HeaderText = "Producto Croqueta";
                dataGridView1.Columns[2].HeaderText = "ID Bulto";
                dataGridView1.Columns[3].HeaderText = "Producto Bulto";
                dataGridView1.Columns[4].HeaderText = "Kilos Bulto";

                var gestorRelacionCroquetaBulto = new GestorRelacionCroquetaBulto();
                var totalRelaciones = gestorRelacionCroquetaBulto.ObtenerTotalRelaciones();

                foreach (var relaciones in totalRelaciones)
                {
                    dataGridView1.Rows.Add(relaciones.IDCroqueta, relaciones.NombreCroqueta,
                        relaciones.IDBulto, relaciones.NombreBulto, relaciones.KilosBultos);
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + " ¡ESTUPID@!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al Ver las relaciones de croquetas vs bultos: " + ex.Message);
            }
        }
    }
}
