using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MASCOSHOP.DTO;
using System.Collections.Generic;

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
            if (ValidarFormatos.ValidarCampoInt(textBox1) == true)
            {
                informarProducto();
            }
            if (textBox1.Text == "")
            {
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("INTRODUCE ALGUN DATO, ¡ESTUPID@!");
            }
            else if (textBox2.Text.Substring(0, 21) == "EL PRODUCTO NO EXISTE")
            {
                MessageBox.Show("INFORMA UN ID CORRECTO, ¡ESTUPID@!");
            }
            else if (Convert.ToDecimal(textBox3.Text) == 0)
            {
                MessageBox.Show("INFORMA UNA CANTIDAD CORRECTA, ¡ESTUPID@!");
            }
            else
            {
                if (RBCantidad.Checked == true)
                {
                    ConfirmarCompraCantidad();
                }
                else if (RBPrecio.Checked == true)
                {
                    ConfirmarCompraPrecio();
                }
                else if (RBAgregarProducto.Checked == true)
                {
                    AgregarProducto();
                }
                else if (RBCancelarVenta.Checked == true)
                {
                    CancelarVenta();
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                RBCantidad.Checked = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ConexionDB c = new ConexionDB();
            Ventas Vta = new Ventas()
            {
                Fecha = DateTime.Today,
                Precio = 0
            };
            c.SelectVentasTotalFecha(Vta);
            textBox4.Text = (Vta.Precio).ToString("C");
            Compras cmps = new Compras()
            {
                Precio = 0
            };
            c.SelectComprasTotal(cmps);
            Ajustes Ajs = new Ajustes()
            {
                Precio_venta = 0,
                Precio_ganancia = 0
            };
            c.SelectAjustesTotal(Ajs);
            Ajustes AjsDia = new Ajustes()
            {
                Precio_venta = 0,
                Precio_ganancia = 0
            };
            c.SelectAjustesdia(AjsDia);
            Ventas VtaT = new Ventas()
            {
                Precio = 0,
                Ganancia = 0
            };
            c.SelectVentasTotal(VtaT);
            Ventas VtaDia = new Ventas()
            {
                Precio = 0,
                Ganancia = 0
            };
            c.SelectVentasdia(VtaDia);
            textBox5.Text = (VtaT.Precio + Ajs.Precio_venta - cmps.Precio).ToString("C");
            textBox6.Text = (VtaDia.Ganancia + AjsDia.Precio_ganancia).ToString("C");
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            ValidarFormatos.ValidarCampoDecimal(textBox3);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
        }
        private void CancelarVenta()
        {
            ConexionDB c = new ConexionDB();
            int ID = Convert.ToInt32(textBox1.Text);
            decimal Cantidad = Convert.ToDecimal(textBox3.Text);
            Ventas Ventatotal = new Ventas()
            {
                ID = ID,
                Cantidad = Cantidad,
                Precio = 0,
                Ganancia = 0,
                Fecha = DateTime.Today
                //Por si es necesario eliminar una compra en una fecha diferente
                //Fecha = DateTime.Parse("2022-09-24")
            };
            if (c.SelectVentasTotalIDCantidadFecha(Ventatotal))
            {
                MessageBox.Show("Cantidad total a eliminar: " + Ventatotal.Cantidad.ToString() +
                ", Precio total a eliminar: " + Ventatotal.Precio.ToString() +
                ", Ganancia total a eliminar: " + Ventatotal.Ganancia.ToString());
                Ventas Ventas = new Ventas()
                {
                    ID = ID,
                    Cantidad = Cantidad,
                    Precio = 0,
                    Ganancia = 0,
                    Fecha = Ventatotal.Fecha
                };
                if (c.DeleteVentasIDCantidadFecha(Ventas))
                {
                    Inventario Inventario = new Inventario()
                    {
                        ID = ID,
                    };
                    if (c.SelectInventarioID(Inventario))
                    {
                        Inventario.Venta -= Ventatotal.Cantidad;
                        Inventario.Existencia += Ventatotal.Cantidad;
                        c.UpddateInventarioID(Inventario);
                    }
                }
            }
        }
        private void AgregarProducto()
        {
            ConexionDB c = new ConexionDB();
            decimal Cantidad = Convert.ToDecimal(textBox3.Text);
            Inventario Inventario = new Inventario()
            {
                ID = Convert.ToInt32(textBox1.Text),
            };
            if (c.SelectInventarioID(Inventario))
            {
                Inventario.Compra += Cantidad;
                Inventario.Existencia += Cantidad;
                c.UpddateInventarioID(Inventario);
            }
        }
        private void ConfirmarCompraPrecio()
        {
            ConexionDB c = new ConexionDB();
            int ID = Convert.ToInt32(textBox1.Text);
            decimal Precio = Convert.ToDecimal(textBox3.Text);
            Precios Precios = new Precios()
            {
                ID = ID
            };
            if (c.SelectPreciosID(Precios))
            {
                Ventas Ventas = new Ventas()
                {
                    ID = ID,
                    Cantidad = Precio / Precios.PrecioVenta,
                    Precio = Precio,
                    Ganancia = (Precio / Precios.PrecioVenta) * (Precios.PrecioVenta - Precios.PrecioCompra),
                    Fecha = DateTime.Today
                };
                Inventario Inventario = new Inventario()
                {
                    ID = ID,
                };
                CambioBultoACroquetas(Inventario, Ventas.Cantidad, c);
                if (c.SelectInventarioID(Inventario))
                {
                    decimal ExistenciaActual = Inventario.Existencia;
                    Inventario.Venta += Ventas.Cantidad;
                    Inventario.Existencia -= Ventas.Cantidad;
                    if (Inventario.Existencia >= 0)
                    {
                        if (Inventario.Existencia == 0)
                        {
                            MessageBox.Show("Ultimo producto vendido");
                        }
                        if (c.InsertVenta(Ventas))
                        {
                            c.UpddateInventarioID(Inventario);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Inventario insuficiente, existencia actual: " + ExistenciaActual);
                    }
                }
            }
        }
        private void ConfirmarCompraCantidad()
        {
            ConexionDB c = new ConexionDB();
            int ID = Convert.ToInt32(textBox1.Text);
            decimal Cantidad = Convert.ToDecimal(textBox3.Text);
            Inventario Inventario = new Inventario()
            {
                ID = ID,
            };
            CambioBultoACroquetas(Inventario, Cantidad, c);
            c.SelectInventarioID(Inventario);
            decimal ExistenciaActual = Inventario.Existencia;
            Inventario.Venta += Cantidad;
            Inventario.Existencia -= Cantidad;
            if (Inventario.Existencia >= 0)
            {
                if (Inventario.Existencia == 0)
                {
                    MessageBox.Show("Ultimo producto vendido");
                }
                Precios Precios = new Precios()
                {
                    ID = ID
                };
                if (c.SelectPreciosID(Precios))
                {
                    Ventas Ventas = new Ventas()
                    {
                        ID = ID,
                        Cantidad = Cantidad,
                        Precio = Cantidad * Precios.PrecioVenta,
                        Ganancia = Cantidad * (Precios.PrecioVenta - Precios.PrecioCompra),
                        Fecha = DateTime.Today
                    };
                    if (c.InsertVenta(Ventas))
                    {
                        c.UpddateInventarioID(Inventario);
                    }
                }
            }
            else
            {
                MessageBox.Show("Inventario insuficiente, existencia actual: " + ExistenciaActual);
            }
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
            informarProducto();
        }
        private void informarProducto()
        {
            ConexionDB c = new ConexionDB();
            int ID = 0;
            decimal cantidadImporte = 1;
            String producto;
            decimal precioVenta, cantidadPrecio;
            if (ValidarFormatos.ValidarCampoInt(textBox1) == true)
            {
                ID = Convert.ToInt32(textBox1.Text);
            }
            if (ValidarFormatos.ValidarCampoDecimal(textBox3) == true)
            {
                cantidadImporte = Convert.ToDecimal(textBox3.Text);
            }
            if (textBox1.Text != "")
            {
                Productos Prd1 = new Productos()
                {
                    ID = ID,
                    Categoria = "",
                    Subcategoria = "",
                    Producto = ""
                };
                c.SelectProductoID(Prd1);
                producto = Prd1.Producto.ToString();
                cantidadPrecio = cantidadImporte;
                Precios Prc1 = new Precios()
                {
                    ID = ID,
                    PrecioCompra = 0,
                    PrecioVenta = 0
                };
                c.SelectPreciosID(Prc1);
                if (RBCantidad.Checked == true)
                {
                    precioVenta = Prc1.PrecioVenta * cantidadPrecio;
                }
                else if (RBPrecio.Checked == true)
                {
                    precioVenta = Prc1.PrecioVenta * 0 + cantidadPrecio;
                }
                else
                {
                    precioVenta = Prc1.PrecioVenta;
                }
                textBox2.Text = producto + " - " + precioVenta.ToString("C");
            }
            else
            {
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void RBCantidad_CheckedChanged(object sender, EventArgs e)
        {
            informarProducto();
        }

        private void RBPrecio_CheckedChanged(object sender, EventArgs e)
        {
            informarProducto();
        }

        private void RBAgregarProducto_CheckedChanged(object sender, EventArgs e)
        {
            informarProducto();
        }

        private void RBCancelarVenta_CheckedChanged(object sender, EventArgs e)
        {
            informarProducto();
        }
        private void CambioBultoACroquetas(Inventario Inventario, decimal Cantidad, ConexionDB c)
        {
            c.SelectInventarioID(Inventario);
            decimal Existencia = Inventario.Existencia - Cantidad;
            if (Existencia < 0)
            {
                RelacionCroquetaBulto[] RCB = new RelacionCroquetaBulto[10];
                for (int i = 0; i < RCB.Length; i++)
                {
                    RCB[i] = new RelacionCroquetaBulto();
                }
                RCB[0].IDCroqueta = Inventario.ID;
                c.SelectRelacionCroquetaBultoIDCroqueta(RCB);
                if (RCB[0].IDBulto > 0)
                {
                    Inventario[] InvBulto = new Inventario[10];
                    Boolean yaSeActualizo = false;
                    for (int i = 0; i < InvBulto.Length; i++)
                    {
                        InvBulto[i] = new Inventario
                        {
                            ID = RCB[i].IDBulto
                        };
                        if (RCB[i].IDBulto != 0)
                        {
                            c.SelectInventarioID(InvBulto[i]);
                            if ((InvBulto[i].Existencia > 0) && (yaSeActualizo == false))
                            {
                                InvBulto[i].Compra--;
                                InvBulto[i].Existencia--;
                                c.UpddateInventarioID(InvBulto[i]);
                                Inventario.Compra += RCB[i].KilosBultos;
                                Inventario.Existencia += RCB[i].KilosBultos;
                                c.UpddateInventarioID(Inventario);
                                yaSeActualizo = true;
                            }
                        }
                    }
                }
            }
        }

        private void bAgregarVentas_Click(object sender, EventArgs e)
        {
            string rutaArchivo;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos CSV (*.csv)|*.csv";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = ofd.FileName;
            }
            else
            {
                return;
            }
            try
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);

                // Lista para guardar las ventas importadas
                List<string> listaVentas = new List<string>();
                string comentario = "";
                ConexionDB c = new ConexionDB();
                foreach (string linea in lineas.Skip(1)) // Omitimos encabezado
                {
                    string[] datos = linea.Split(',');
                    if (!int.TryParse(datos[0], out int ID) ||
                        !decimal.TryParse(datos[1], out decimal Cantidad) ||
                        !DateTime.TryParse(datos[2], out DateTime fecha))
                    {
                        comentario="El ID, la cantidad o la fecha son invalidas";
                    }
                    else
                    {
                        Inventario Inventario = new Inventario()
                        {
                            ID = int.Parse(datos[0]),
                        };
                        if (c.SelectInventarioID(Inventario))
                        {
                            decimal ExistenciaActual = Inventario.Existencia;
                            Inventario.Venta += Cantidad;
                            Inventario.Existencia -= Cantidad;
                            if (Inventario.Existencia >= 0)
                            {
                                if (Inventario.Existencia == 0)
                                {
                                    comentario = "Ultimo producto vendido";
                                }
                                Precios Precios = new Precios()
                                {
                                    ID = ID
                                };
                                if (c.SelectPreciosID(Precios))
                                {
                                    Ventas Ventas = new Ventas()
                                    {
                                        ID = ID,
                                        Cantidad = Cantidad,
                                        Precio = Cantidad * Precios.PrecioVenta,
                                        Ganancia = Cantidad * (Precios.PrecioVenta - Precios.PrecioCompra),
                                        Fecha = fecha
                                    };
                                    if (c.InsertVenta(Ventas))
                                    {
                                        if (c.UpddateInventarioID(Inventario))
                                        {
                                            comentario = "Venta agregada exitosamente";
                                        }
                                        else
                                        {
                                            comentario = "error al actualizar el inventario";
                                        }
                                    }
                                    else
                                    {
                                        comentario = "error al insertar la venta";
                                    }
                                }
                                else
                                {
                                    comentario = "Error al seleccionar el precio";
                                }
                            }
                            else
                            {
                                comentario = "No hay suficientes productos para la venta";
                            }
                        }
                        else
                        {
                            comentario = "El producto no existe";
                        }
                    }
                    listaVentas.Add(datos[0] + "," + datos[1] + "," + datos[2] + "," + comentario);
                }
                string rutaLog = Path.Combine(Path.GetDirectoryName(rutaArchivo), "resultado_importacion.csv");
                File.WriteAllLines(rutaLog, listaVentas);
                MessageBox.Show("Importación finalizada. Total: " + listaVentas.Count + " ventas.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo: " + ex.Message);
            }
        }
    }
}
