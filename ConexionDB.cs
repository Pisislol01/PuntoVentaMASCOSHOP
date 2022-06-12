using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace MASCOSHOP
{
    class ConexionDB
    {
        SqlConnection cn;
        SqlCommand cmd;
        public ConexionDB()
        {
            cn = new SqlConnection("Data Source=.;Initial Catalog=MASCOSHOP;Integrated Security=True");
        }
        public Boolean DeleteVentasIDCantidadFecha(Ventas vta)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("delete from Ventas where ID = '{0}' and Cantidad = '{1}' and Fecha = '{2}'"
                    , vta.ID, vta.Cantidad, vta.Fecha.ToString("yyyy-MM-dd")), cn);
                cmd.ExecuteNonQuery();
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar ventas ID, Cantidad y Fecha. Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean SelectVentasTotalIDCantidadFecha(Ventas vta)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("Select Sum(Cantidad), sum(Precio), sum(Ganancia) from Ventas where ID = '{0}' and Cantidad = '{1}' and Fecha = '{2}'",
                    vta.ID, vta.Cantidad, vta.Fecha.ToString("yyyy-MM-dd")), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    vta.Cantidad = leer.GetDecimal(0);
                    vta.Precio = leer.GetDecimal(1);
                    vta.Ganancia = leer.GetDecimal(2);
                }
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar ventas ID, Cantidad y Fecha. Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean SelectVentasTotalFecha(Ventas vta)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("Select COALESCE(Sum(Cantidad),0), COALESCE(sum(Precio),0), COALESCE(sum(Ganancia),0) from Ventas where Fecha = '{0}'",
                    vta.Fecha.ToString("yyyy-MM-dd")), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    vta.Cantidad = leer.GetDecimal(0);
                    vta.Precio = leer.GetDecimal(1);
                    vta.Ganancia = leer.GetDecimal(2);
                }
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                MessageBox.Show("Error al buscar ventas Fecha. Mensaje de error: " + ex.ToString());          
                return false;
            }
        }
        public Boolean UpddateInventarioID(Inventario Inv)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("update Inventario set Compra_ini = '{0}', Compra = '{1}', Venta = '{2}', Existencia = '{3}', TimeStampUltimaModificacion = '{4}' where ID = '{5}'"
                    , Inv.CompraIni, Inv.Compra, Inv.Venta, Inv.Existencia, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), Inv.ID), cn);
                cmd.ExecuteNonQuery();
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar en el inventario con el ID: " + Inv.ID + " Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean UpdatePrecios(Precios Prc)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("update Precios set Precio_compra = '{0}', Precio_venta = '{1}', TimeStampUltimaModificacion = '{2}' where ID = '{3}'"
                    , Prc.PrecioCompra, Prc.PrecioVenta, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), Prc.ID), cn);
                cmd.ExecuteNonQuery();
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los precios con el ID: " + Prc.ID + " Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean SelectInventarioID(Inventario Inv)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("Select Compra_ini, Compra, Venta, Existencia from Inventario where ID = '{0}'",
                    Inv.ID), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    Inv.CompraIni = leer.GetDecimal(0);
                    Inv.Compra = leer.GetDecimal(1);
                    Inv.Venta = leer.GetDecimal(2);
                    Inv.Existencia = leer.GetDecimal(3);
                }
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar un producto en el inventario con el ID: " + Inv.ID + " Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean InsertVenta(Ventas Ventas)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("Insert Into Ventas(ID, Cantidad, Precio, Ganancia, Fecha) values('{0}', '{1}', '{2}', '{3}', '{4}')",
                    Ventas.ID, Ventas.Cantidad, Ventas.Precio, Ventas.Ganancia, Ventas.Fecha.ToString("yyyy-MM-dd")), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar una venta, Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean InsertCompra(Compras Cpr)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("Insert Into Compras(Descripcion, Precio, Fecha) values ('{0}','{1}','{2}')",
                    Cpr.Descripcion, Cpr.Precio, Cpr.Fecha.ToString("yyyy-MM-dd")), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar una Compra, Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean InserAjuste(Ajustes Ajs)
        {
            try
            {
                AbrirConexion();                
                cmd = new SqlCommand(string.Format("Insert Into Ajustes(Descripcion, Precio_venta, Precio_ganancia, Fecha) values ('{0}','{1}','{2}','{3}')",
                    Ajs.Descripcion, Ajs.Precio_venta, Ajs.Precio_ganancia, Ajs.Fecha.ToString("yyyy-MM-dd")), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar un Ajuste, Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean InsertProducto(Productos Pdt)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("Insert Into Productos(Categoria, Subcategoria, Producto) values ('{0}','{1}','{2}')",
                    Pdt.Categoria,Pdt.Subcategoria,Pdt.Producto), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                CerrarConexion();
                SelectIdMax(Pdt);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar un Producto, Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean SelectIdMax(Productos Pdt2)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("select max(ID) from Productos"), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    Pdt2.ID = leer.GetInt32(0);
                }
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el ID nuevo en Productos, Mensaje de error: " + ex.ToString());
                return true;
            }
        }
        public Boolean InsertInventario(Inventario Inv)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("Insert Into Inventario(ID, Compra_ini, Compra, Venta, Existencia) values ('{0}','{1}','{2}','{3}','{4}')",
                    Inv.ID,Inv.CompraIni,Inv.Compra,Inv.Venta,Inv.Existencia), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar un Producto, Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean InsertPrecios(Precios Prc)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("Insert Into Precios(ID, Precio_compra, Precio_venta) values ('{0}','{1}','{2}')",
                    Prc.ID,Prc.PrecioCompra,Prc.PrecioVenta), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar Precios, Mensaje de error: " + ex.ToString());
                return false;
            }
        }
        public Boolean SelectPreciosID(Precios Precios)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("select Precio_compra, Precio_venta from Precios where ID = '{0}'",
                    Precios.ID), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    Precios.PrecioCompra = leer.GetDecimal(0);
                    Precios.PrecioVenta = leer.GetDecimal(1);
                }
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar un precio con el ID: " + Precios.ID + " Mensaje de error: " + ex.ToString());
                return true;
            }
        }
        public void SelectProductoID(Productos Prs1)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("select Categoria, Subcategoria, Producto from Productos where ID = '{0}'",
                    Prs1.ID), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    Prs1.Categoria = leer.GetString(0);
                    Prs1.Subcategoria = leer.GetString(1);
                    Prs1.Producto = leer.GetString(2);
                }
                else
                {
                    Prs1.Producto = "EL PRODUCTO NO EXISTE";
                }
                CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar un producto con el ID: " + Prs1.ID + " Mensaje de error: " + ex.ToString());
            }
        }
        public Boolean SelectComprasTotal(Compras cmps)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("Select Sum(Precio) from Compras"), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    cmps.Precio = leer.GetDecimal(0);
                }
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                MessageBox.Show("Error al obtener el total de compras: " + ex.ToString());
                return false;
            }
        }
        public Boolean SelectVentasTotal(Ventas Vnts)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("select sum(Cantidad), sum(Precio), sum(Ganancia) from Ventas"), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    Vnts.Cantidad = leer.GetDecimal(0);
                    Vnts.Precio = leer.GetDecimal(1);
                    Vnts.Ganancia = leer.GetDecimal(2);
                }
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                MessageBox.Show("Error al obtener el total de Ventas: " + ex.ToString());
                return false;
            }
        }
        public Boolean SelectAjustesTotal(Ajustes Ajs)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("select sum(Precio_venta), Sum(Precio_ganancia) from Ajustes"), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    Ajs.Precio_venta = leer.GetDecimal(0);
                    Ajs.Precio_ganancia = leer.GetDecimal(1);
                }
                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                CerrarConexion();
                MessageBox.Show("Error al obtener el total de ajustes: " + ex.ToString());
                return false;
            }
        }
        public void AbrirConexion()
        {
            try
            {
                cn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto la BD: " + ex.ToString());
            }
        }
        public void CerrarConexion()
        {
            try
            {
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto la BD: " + ex.ToString());
            }
        }
        public void BuscarProductoNombre(DataGridView DGV, string buscar)
        {
            try
            {
                int i = 0;
                buscar = '%' + buscar + '%';
                AbrirConexion();
                cmd = new SqlCommand(string.Format("SELECT p.ID, p.Categoria, p.Subcategoria, p.Producto, r.Precio_venta , i.Existencia FROM Productos p, Precios r, Inventario i where p.ID = r.ID and p.ID = i.ID and p.Producto like '{0}'",
                    buscar), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                while (leer.Read())
                {
                    DGV.Rows.Add();
                    DGV[0, i].Value = Convert.ToString(leer.GetInt32(0));
                    DGV[1, i].Value = leer.GetString(1);
                    DGV[2, i].Value = leer.GetString(2);
                    DGV[3, i].Value = leer.GetString(3);
                    DGV[4, i].Value = Convert.ToString(leer.GetDecimal(4));
                    DGV[5, i].Value = Convert.ToString(leer.GetDecimal(5));
                    i++;
                }
                CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscarProductoNombre: " + ex.ToString());
            }
        }
        public void VerCompras(DataGridView DGV)
        {
            try
            {
                int i = 0;
                AbrirConexion();
                cmd = new SqlCommand(string.Format("SELECT ID, Descripcion, Precio, Fecha FROM Compras order by ID"), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                while (leer.Read())
                {
                    DGV.Rows.Add();
                    DGV[0, i].Value = Convert.ToString(leer.GetInt32(0));
                    DGV[1, i].Value = leer.GetString(1);
                    DGV[2, i].Value = Convert.ToString(leer.GetDecimal(2));
                    DGV[3, i].Value = leer.GetDateTime(3).ToString("yyyy-MM-dd");
                    i++;
                }
                CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Ver Compras: " + ex.ToString());
            }
        }
        public void VerAjustes(DataGridView DGV)
        {
            try
            {
                int i = 0;
                AbrirConexion();
                cmd = new SqlCommand(string.Format("SELECT ID, Descripcion, Precio_venta, Precio_ganancia, Fecha FROM Ajustes order by ID"), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                while (leer.Read())
                {
                    DGV.Rows.Add();
                    DGV[0, i].Value = Convert.ToString(leer.GetInt32(0));
                    DGV[1, i].Value = leer.GetString(1);
                    DGV[2, i].Value = Convert.ToString(leer.GetDecimal(2));
                    DGV[3, i].Value = Convert.ToString(leer.GetDecimal(3));
                    DGV[4, i].Value = leer.GetDateTime(4).ToString("yyyy-MM-dd");
                    i++;
                }
                CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Ver Ajustes: " + ex.ToString());
            }

        }
        public void BuscarCompras(string Fecha, DataGridView DGV)
        {
            try
            {
                int i = 0;
                decimal Cantidad = 0, Precio = 0;
                AbrirConexion();
                cmd = new SqlCommand(string.Format("SELECT p.ID, p.Producto, v.Cantidad, v.Precio, v.Fecha FROM Productos p, Ventas v where p.ID = v.ID and v.Fecha = '{0}' order by v.TimeStampUltimaModificacion",
                    Fecha), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                while (leer.Read())
                {
                    DGV.Rows.Add();
                    DGV[0, i].Value = Convert.ToString(leer.GetInt32(0));
                    DGV[1, i].Value = leer.GetString(1);
                    DGV[2, i].Value = Convert.ToString(leer.GetDecimal(2));
                    DGV[3, i].Value = Convert.ToString(leer.GetDecimal(3));
                    DGV[4, i].Value = leer.GetDateTime(4).ToString("yyyy-MM-dd");
                    i++;
                    Cantidad += leer.GetDecimal(2);
                    Precio += leer.GetDecimal(3);
                }
                CerrarConexion();
                DGV.Rows.Add();
                DGV[1, i].Value = "Total";
                DGV[2, i].Value = Cantidad.ToString();
                DGV[3, i].Value = Precio.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Buscar Compras: " + ex.ToString());
            }
        }
        public void buscarCategorias(ComboBox cB)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("SELECT Categoria FROM Productos group by Categoria")
                    , cn);
                SqlDataReader leer = cmd.ExecuteReader();
                while (leer.Read())
                {
                    cB.Items.Add(leer.GetString(0));
                }
                CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Buscar Categorias: " + ex.ToString());
            }
        }
        public void buscarSubCategoria(ComboBox cB)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("SELECT Subcategoria FROM Productos group by Subcategoria")
                    , cn);
                SqlDataReader leer = cmd.ExecuteReader();
                while (leer.Read())
                {
                    cB.Items.Add(leer.GetString(0));
                }
                CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Buscar Subcategorias: " + ex.ToString());
            }
        }
    }
}
