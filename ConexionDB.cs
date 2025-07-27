using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using MASCOSHOP.DTO;
namespace MASCOSHOP
{
    class ConexionDB
    {
        SqlConnection cn;
        SqlCommand cmd;
        public ConexionDB()
        {
            //cn = new SqlConnection("Data Source=LAPTOP-MGB6AQEI;Initial Catalog=MASCOSHOP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //cn = new SqlConnection("Data Source=LAPTOP-MGB6AQEI;Initial Catalog=MASCOSHOP_Pruebas;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            cn = new SqlConnection("Data Source=(local)\\SQLEXPRESS;Initial Catalog=MASCOSHOP;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
        }
        public void DeleteVentasPorIdCantidadFecha(Ventas vta)
        {
            try
            {
                AbrirConexion();

                using var cmd = new SqlCommand(@"
            DELETE
              FROM Ventas
             WHERE ID       = @ID
               AND Cantidad = @Cantidad
               AND Fecha    = @Fecha", cn);

                // Parámetros para evitar inyección y formateos incorrectos
                cmd.Parameters.AddWithValue("@ID", vta.ID);
                cmd.Parameters.AddWithValue("@Cantidad", vta.Cantidad);
                cmd.Parameters.AddWithValue("@Fecha", vta.Fecha.Date);

                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas < 1)
                {
                    throw new InvalidOperationException(
                        $"No se encontró ninguna venta para ID={vta.ID}, Cantidad={vta.Cantidad}, Fecha={vta.Fecha:yyyy-MM-dd}.");
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    $"Error SQL al eliminar ventas para ID={vta.ID}, Cantidad={vta.Cantidad}, Fecha={vta.Fecha:yyyy-MM-dd}: {ex.Message}", ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void ObtenerTotalVentasCancelar(Ventas vta)
        {
            try
            {
                AbrirConexion();

                using var cmd = new SqlCommand(@"
            SELECT 
                SUM(Cantidad)   AS TotalCantidad,
                SUM(Precio)     AS TotalPrecio,
                SUM(Ganancia)   AS TotalGanancia,
                count(*)        AS TotalRegistros
              FROM Ventas
             WHERE ID       = @ID
               AND Cantidad = @Cantidad
               AND Fecha    = @Fecha", cn);

                cmd.Parameters.AddWithValue("@ID", vta.ID);
                cmd.Parameters.AddWithValue("@Cantidad", vta.Cantidad);
                cmd.Parameters.AddWithValue("@Fecha", vta.Fecha.Date);

                using var reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    // (No debería ocurrir porque SUM siempre devuelve fila, pero lo contemplamos)
                    throw new InvalidOperationException(
                        $"No hay ventas para ID={vta.ID}, cantidad={vta.Cantidad}, fecha={vta.Fecha:yyyy-MM-dd}.");
                }
                // Si SUM devuelve NULL significa que no hay registros coincidentes
                if (reader.IsDBNull(0))
                {
                    throw new InvalidOperationException(
                        $"No hay ventas para ID={vta.ID}, cantidad={vta.Cantidad}, fecha={vta.Fecha:yyyy-MM-dd}.");
                }
                // Mapea los totales leídos
                vta.Cantidad = reader.GetDecimal(0);
                vta.Precio = reader.GetDecimal(1);
                vta.Ganancia = reader.GetDecimal(2);
                vta.ID = reader.GetInt32(3);
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    $"Error SQL al obtener totales de ventas para ID={vta.ID}, cantidad={vta.Cantidad}, fecha={vta.Fecha:yyyy-MM-dd}: {ex.Message}", ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public Ventas SelectVentasTotalFecha(DateTime fecha)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand("SELECT COALESCE(SUM(Cantidad), 0), COALESCE(SUM(Precio), 0), COALESCE(SUM(Ganancia), 0) FROM Ventas WHERE Fecha = @Fecha", cn);
                cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("yyyy-MM-dd"));

                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read())
                {
                    return new Ventas
                    {
                        Fecha = fecha,
                        Cantidad = leer.GetDecimal(0),
                        Precio = leer.GetDecimal(1),
                        Ganancia = leer.GetDecimal(2)
                    };
                }
                else
                {
                    throw new InvalidOperationException($"No hay ventas registradas en la fecha: {fecha:yyyy-MM-dd}");
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException($"Error al consultar ventas por fecha {fecha:yyyy-MM-dd}: {ex.Message}", ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void UpddateInventarioID(Inventario Inv)
        {
            try
            {
                AbrirConexion();

                using var cmd = new SqlCommand("UPDATE Inventario " +
                    "SET Compra_ini = @CompraIni, " +
                    "Compra = @Compra, " +
                    "Venta = @Venta, " +
                    "Existencia = @Existencia, " +
                    "TimeStampUltimaModificacion = @TimeStampUltimaModificacion " +
                    "WHERE ID = @ID", cn);

                cmd.Parameters.AddWithValue("@CompraIni", Inv.CompraIni);
                cmd.Parameters.AddWithValue("@Compra", Inv.Compra);
                cmd.Parameters.AddWithValue("@Venta", Inv.Venta);
                cmd.Parameters.AddWithValue("@Existencia", Inv.Existencia);
                cmd.Parameters.AddWithValue("@TimeStampUltimaModificacion", DateTime.Now);
                cmd.Parameters.AddWithValue("@ID", Inv.ID);

                int filas = cmd.ExecuteNonQuery();
                if (filas != 1)
                {
                    throw new InvalidOperationException("No se actualizó el inventario con el ID: " + Inv.ID + ". Filas afectadas: " + filas);
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al actualizar en el inventario con el ID: " + Inv.ID + " Mensaje de error: " + ex.ToString());
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void UpddateInventarioID2(Inventario inv)
        {
            try
            {
                AbrirConexion();
                using var cmd = new SqlCommand(@"
                UPDATE Inventario 
                   SET Compra_ini                 = @CompraIni
                     , Compra                     = @Compra
                     , Venta                      = @Venta
                     , Existencia                 = @Existencia
                     , TimeStampUltimaModificacion = @Modificado
                 WHERE ID = @ID",
                     cn);

                // Parámetros para evitar inyección y formateos incorrectos
                cmd.Parameters.AddWithValue("@CompraIni", inv.CompraIni);
                cmd.Parameters.AddWithValue("@Compra", inv.Compra);
                cmd.Parameters.AddWithValue("@Venta", inv.Venta);
                cmd.Parameters.AddWithValue("@Existencia", inv.Existencia);
                cmd.Parameters.AddWithValue("@Modificado", DateTime.Now);
                cmd.Parameters.AddWithValue("@ID", inv.ID);

                int filas = cmd.ExecuteNonQuery();
                if (filas != 1)
                {
                    throw new InvalidOperationException(
                        $"No se actualizó inventario (ID={inv.ID}). Filas afectadas: {filas}.");
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    $"Error SQL al actualizar inventario (ID={inv.ID}): {ex.Message}", ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void UpdatePrecios(Precios prc)
        {
            try
            {
                AbrirConexion();

                using (var cmd = new SqlCommand(
                    "UPDATE Precios SET Precio_compra = @Precio_compra, Precio_venta = @Precio_venta, " +
                    "TimeStampUltimaModificacion = @TimeStampUltimaModificacion WHERE ID = @ID", cn)) // ✅ se incluye la conexión
                {
                    cmd.Parameters.AddWithValue("@Precio_compra", prc.PrecioCompra);
                    cmd.Parameters.AddWithValue("@Precio_venta", prc.PrecioVenta);
                    cmd.Parameters.AddWithValue("@TimeStampUltimaModificacion", DateTime.Now); // ✅ sin ToString
                    cmd.Parameters.AddWithValue("@ID", prc.ID);

                    int filas = cmd.ExecuteNonQuery();
                    if (filas != 1)
                    {
                        throw new InvalidOperationException(
                            $"Error al actualizar el precio para el ID={prc.ID}. Filas afectadas: {filas}.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    $"Error SQL al actualizar los Precios (ID={prc.ID}): {ex.Message}", ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public Inventario SelectInventarioID2(int idProducto)
        {
            try
            {
                AbrirConexion();

                using (var cmd = new SqlCommand(@"
            SELECT Compra_ini, Compra, Venta, Existencia,
                   TimeStampAlta, TimeStampUltimaModificacion
              FROM Inventario
             WHERE ID = @ID", cn))
                {
                    cmd.Parameters.AddWithValue("@ID", idProducto);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new InvalidOperationException(
                                $"No existe inventario para el producto con ID={idProducto}.");
                        }
                        var inventario = new Inventario
                        {
                            ID = idProducto,
                            CompraIni = reader.GetDecimal(0),
                            Compra = reader.GetDecimal(1),
                            Venta = reader.GetDecimal(2),
                            Existencia = reader.GetDecimal(3),
                            TimeStampAlta = reader.GetDateTime(4),
                            TimeStampUltimaModificacion = reader.GetDateTime(5)
                        };
                        return inventario;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    $"Error SQL al leer inventario para ID={idProducto}: {ex.Message}", ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void InsertVenta(Ventas venta)
        {
            try
            {
                AbrirConexion();
                using (var cmd = new SqlCommand(
                    "INSERT INTO Ventas (ID, Cantidad, Precio, Ganancia, Fecha, TimeStampUltimaModificacion) " +
                    "VALUES (@ID,@Cantidad,@Precio,@Ganancia,@Fecha,@Modificado)", cn))
                {
                    cmd.Parameters.AddWithValue("@ID", venta.ID);
                    cmd.Parameters.AddWithValue("@Cantidad", venta.Cantidad);
                    cmd.Parameters.AddWithValue("@Precio", venta.Precio);
                    cmd.Parameters.AddWithValue("@Ganancia", venta.Ganancia);
                    cmd.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    cmd.Parameters.AddWithValue("@Modificado", DateTime.Now);

                    int filas = cmd.ExecuteNonQuery();
                    if (filas != 1)
                        throw new InvalidOperationException("No se insertó ninguna fila al registrar la venta.");
                }
            }
            catch (SqlException ex)
            {
                // relanzamos con más contexto
                throw new InvalidOperationException($"Error SQL al insertar la venta (ID={venta.ID}): {ex.Message}", ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void InsertCompra(Compras Cpr)
        {
            try
            {
                AbrirConexion();

                using (var cmd = new SqlCommand(
                    "INSERT INTO Compras (" +
                    "ID, Descripcion, Precio, Fecha, TimeStampAlta) " +
                    "VALUES (@ID, @Descripcion, @Precio, @Fecha, @TimeStampAlta)", cn))
                {
                    cmd.Parameters.AddWithValue("@ID", Cpr.ID);
                    cmd.Parameters.AddWithValue("@Descripcion", Cpr.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", Cpr.Precio);
                    cmd.Parameters.AddWithValue("@Fecha", Cpr.Fecha);
                    cmd.Parameters.AddWithValue("@TimeStampAlta", DateTime.Now);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas != 1)
                    {
                        throw new InvalidOperationException("No se insertó ninguna fila al registrar la compra.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al insertar una Compra. Detalles: " + ex.Message, ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void InsertAjuste(Ajustes Ajs)
        {
            try
            {
                AbrirConexion();

                using(var cmd = new SqlCommand(
                    "Insert Into Ajustes(" +
                    "ID, Descripcion, Precio_venta, Precio_ganancia, Fecha, TimeStampAlta) " +
                    "values (@ID, @Descripcion, @PrecioVenta, @PrecioGanancia, @Fecha, @TimeStampAlta)", cn))
                {
                    cmd.Parameters.AddWithValue("@ID", Ajs.ID);
                    cmd.Parameters.AddWithValue("@Descripcion", Ajs.Descripcion);
                    cmd.Parameters.AddWithValue("@PrecioVenta", Ajs.Precio_venta);
                    cmd.Parameters.AddWithValue("@PrecioGanancia", Ajs.Precio_ganancia);
                    cmd.Parameters.AddWithValue("@Fecha", Ajs.Fecha);
                    cmd.Parameters.AddWithValue("@TimeStampAlta", DateTime.Now);
                    int filas = cmd.ExecuteNonQuery();
                    if (filas != 1)
                        throw new InvalidOperationException("No se insertó ninguna fila al registrar el ajuste.");
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al insertar un Ajuste, Detalles: "
                    + ex.ToString(), ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void InsertProducto(Productos Pdt)
        {
            try
            {
                AbrirConexion();
                using (var cmd = new SqlCommand(
                    "Insert Into Productos(" +
                    "ID, Categoria, Subcategoria, Producto, TimeStampAlta) " +
                    "values (@ID,@Categoria,@Subcategoria,@Producto,@TimeStampAlta)", cn))
                {
                    cmd.Parameters.AddWithValue("@ID", Pdt.ID);
                    cmd.Parameters.AddWithValue("@Categoria", Pdt.Categoria);
                    cmd.Parameters.AddWithValue("@Subcategoria", Pdt.Subcategoria);
                    cmd.Parameters.AddWithValue("@Producto", Pdt.Producto);
                    cmd.Parameters.AddWithValue("@TimeStampAlta", DateTime.Now);
                    int filas = cmd.ExecuteNonQuery();
                    if(filas != 1)
                        throw new InvalidOperationException("No se insertó ninguna fila al registrar el producto.");
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al insertar un Producto, Mensaje de error: " + ex.ToString());
            }
            finally
            {
                CerrarConexion();
            }
        }
        public int ObtenerIdNuevoProducto()
        {
            int id = 0;
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("select max(ID) from Productos"), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    id = leer.GetInt32(0);
                }
                return id+1;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al buscar el ID nuevo en Productos, Mensaje de error: " + ex.ToString());
            }
            finally
            {
                CerrarConexion();
            }
        }
        public int SelectIdMaxAjuste()
        {
            int id = 0;
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(string.Format("select max(ID) from Ajustes"), cn);
                SqlDataReader leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    id = leer.GetInt32(0);
                }
                return id;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al buscar el ID maximo de los ajustes, Mensaje de error: " + ex.ToString());
            }
            finally
            {
                CerrarConexion();
            }
        }
        public int SelectIdMaxCompras()
        {
            try
            {
                AbrirConexion();

                using (var cmd = new SqlCommand("SELECT MAX(ID) FROM Compras", cn))
                {
                    object resultado = cmd.ExecuteScalar();

                    // Verificamos si el resultado no es DBNull
                    if (resultado != DBNull.Value && resultado != null)
                    {
                        return Convert.ToInt32(resultado);
                    }
                    else
                    {
                        return 0; // No hay registros en la tabla Compras
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al buscar el ID máximo de las compras. Detalles: " + ex.Message, ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void InsertInventario(int idNuevoProducto,int numeroProductosComprados)
        {
            try
            {
                AbrirConexion();
                using(var cmd = new SqlCommand(
                    "Insert Into Inventario(" +
                    "ID, Compra_ini, Compra, Venta, Existencia, TimeStampUltimaModificacion) " +
                    "values (@ID,@Compra_ini,@Compra,@venta,@Existencia,@TimeStampUltimaModificacion)",
                    cn))
                {
                    cmd.Parameters.AddWithValue("@ID", idNuevoProducto);
                    cmd.Parameters.AddWithValue("@Compra_ini", 0);
                    cmd.Parameters.AddWithValue("@Compra", numeroProductosComprados);
                    cmd.Parameters.AddWithValue("@Venta", 0);
                    cmd.Parameters.AddWithValue("@Existencia", numeroProductosComprados);
                    cmd.Parameters.AddWithValue("@TimeStampUltimaModificacion", DateTime.Now);
                    int filas = cmd.ExecuteNonQuery();
                    if (filas != 1)
                        throw new InvalidOperationException("No se insertó ninguna fila al registrar el inventario.");
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al insertar un Producto, Mensaje de error: " + ex.ToString());
            }
            finally
            {
                CerrarConexion();
            }
        }
        public void InsertPrecios(Precios Prc)
        {
            try
            {
                AbrirConexion();
                using( var cmd = new SqlCommand(
                    "Insert Into Precios(" +
                    "ID, Precio_compra, Precio_venta, TimeStampAlta,TimeStampUltimaModificacion) " +
                    "values (@ID,@PrecioCompra,@PrecioVenta,@TimeStampAlta,@TimeStampUltimaModificacion)", cn))
                {
                    cmd.Parameters.AddWithValue("@ID", Prc.ID);
                    cmd.Parameters.AddWithValue("@PrecioCompra", Prc.PrecioCompra);
                    cmd.Parameters.AddWithValue("@PrecioVenta", Prc.PrecioVenta);
                    cmd.Parameters.AddWithValue("@TimeStampAlta", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TimeStampUltimaModificacion", DateTime.Now);
                    int filas = cmd.ExecuteNonQuery();
                    if (filas != 1)
                        throw new InvalidOperationException("No se insertó ninguna fila al registrar los precios.");
                }             
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al insertar Precios, Mensaje de error: " + ex.ToString());
            }
            finally
            {
                CerrarConexion();
            }
        }
        public Precios SelectPreciosID2(int idProducto)
        {
            try
            {
                AbrirConexion();
                using var cmd = new SqlCommand(@"
            SELECT Precio_compra, Precio_venta, TimeStampAlta, TimeStampUltimaModificacion
              FROM Precios
             WHERE ID = @ID", cn);
                cmd.Parameters.AddWithValue("@ID", idProducto);

                using var reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    // En vez de return false, lanzamos excepción
                    throw new InvalidOperationException(
                        $"No existen precios para el ID={idProducto}.");
                }

                // Si llegamos aquí, sí hay datos: mapeamos
                var precios = new Precios
                {
                    ID = idProducto,
                    PrecioCompra = reader.GetDecimal(0),
                    PrecioVenta = reader.GetDecimal(1),
                    TimeStampAlta = reader.GetDateTime(2),
                    TimeStampUltimaModificacion = reader.GetDateTime(3),
                };
                return precios;
            }
            catch (SqlException ex)
            {
                // Errores de SQL o de conexión
                throw new InvalidOperationException(
                    $"Error SQL al leer precios para ID={idProducto}: {ex.Message}", ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public Compras SelectTotalCompras()
        {
            var compras = new Compras();

            try
            {
                AbrirConexion();

                using (var cmd = new SqlCommand("SELECT COALESCE(SUM(Precio), 0) FROM Compras", cn))
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        compras.Precio = reader.GetDecimal(0);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error SQL al obtener el total de compras.", ex);
            }
            finally
            {
                CerrarConexion();
            }

            return compras;
        }
        public Ventas SelectTotalVentas()
        {
            var resultado = new Ventas();

            try
            {
                AbrirConexion();

                using (var cmd = new SqlCommand("SELECT COALESCE(SUM(Cantidad), 0), COALESCE(SUM(Precio), 0), COALESCE(SUM(Ganancia), 0) FROM Ventas", cn))
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resultado.Cantidad = reader.GetDecimal(0);
                        resultado.Precio = reader.GetDecimal(1);
                        resultado.Ganancia = reader.GetDecimal(2);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error SQL al obtener el total de ventas.", ex);
            }
            finally
            {
                CerrarConexion();
            }

            return resultado;
        }
        public Ajustes SelectTotalAjustes()
        {
            var resultado = new Ajustes();

            try
            {
                AbrirConexion();
                using (var cmd = new SqlCommand("SELECT COALESCE(SUM(Precio_venta), 0), COALESCE(SUM(Precio_ganancia), 0) FROM Ajustes", cn))
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resultado.Precio_venta = reader.GetDecimal(0);
                        resultado.Precio_ganancia = reader.GetDecimal(1);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error SQL al obtener el total de ajustes.", ex);
            }
            finally
            {
                CerrarConexion();
            }

            return resultado;
        }
        public Ajustes SelectAjustesPorFecha(DateTime fecha)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand("SELECT COALESCE(SUM(Precio_venta), 0), COALESCE(SUM(Precio_ganancia), 0) FROM Ajustes WHERE Fecha = @Fecha", cn);
                cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("yyyy-MM-dd"));

                using (SqlDataReader leer = cmd.ExecuteReader())
                {
                    if (leer.Read())
                    {
                        return new Ajustes
                        {
                            Precio_venta = leer.GetDecimal(0),
                            Precio_ganancia = leer.GetDecimal(1),
                            Fecha = fecha
                        };
                    }
                    else
                    {
                        throw new InvalidOperationException($"No se encontraron ajustes para la fecha {fecha:yyyy-MM-dd}.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al obtener los ajustes: " + ex.Message, ex);
            }
            finally
            {
                CerrarConexion();
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
                throw new Exception("No se conecto la BD: " + ex.ToString());
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
                throw new Exception("No se conecto la BD: " + ex.ToString());
            }
        }
        public List<ProductoDTO> BuscarProductoNombre(string buscar)
        {
            var lista = new List<ProductoDTO>();
            try
            {
                buscar = '%' + buscar + '%';
                AbrirConexion();
                cmd = new SqlCommand("SELECT p.ID, p.Categoria, p.Subcategoria, p.Producto, r.Precio_venta," +
                    " i.Existencia FROM Productos p, Precios r, Inventario i " +
                    "WHERE p.ID = r.ID AND p.ID = i.ID AND p.Producto LIKE @buscar", cn);
                cmd.Parameters.AddWithValue("@buscar", buscar);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new ProductoDTO
                    {
                        ID = reader.GetInt32(0),
                        Categoria = reader.GetString(1),
                        Subcategoria = reader.GetString(2),
                        Producto = reader.GetString(3),
                        PrecioVenta = reader.GetDecimal(4),
                        Existencia = reader.GetDecimal(5)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
            return lista;
        }
        public List<Compras> SelectCompras()
        {
            try
            {
                AbrirConexion();
                using var cmd = new SqlCommand("SELECT ID, Descripcion, Precio, Fecha " +
                    "FROM Compras ORDER BY ID desc", cn);
                using var reader = cmd.ExecuteReader();
                var resultados = new List<Compras>();
                while (reader.Read())
                {
                    var compra = new Compras
                    {
                        ID = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        Precio = reader.GetDecimal(2),
                        Fecha = reader.GetDateTime(3)
                    };
                    resultados.Add(compra);
                }
                return resultados;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al obtener las compras: " + ex.Message, ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public List<Ajustes> ObtenerAjustes()
        {
            try
            {
                AbrirConexion();

                using var cmd = new SqlCommand("SELECT ID, Descripcion, Precio_venta, Precio_ganancia," +
                    " Fecha FROM Ajustes order by ID desc", cn);
                using var reader = cmd.ExecuteReader();
                var resultado = new List<Ajustes>();
                while (reader.Read())
                {
                    var ajustes = new Ajustes
                    {
                        ID = reader.GetInt32(0),
                        Descripcion = reader.GetString(1),
                        Precio_venta = reader.GetDecimal(2),
                        Precio_ganancia = reader.GetDecimal(3),
                        Fecha = reader.GetDateTime(4)
                    };
                    resultado.Add(ajustes);
                }
                return resultado;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException
                ("Error al obtener todos los Ajustes: " + ex.ToString());
            }
            finally
            {
                CerrarConexion();
            }

        }
        public List<VentasProductos> SelectVentasPorFecha(DateTime Fecha)
        {
            try
            {
                AbrirConexion();
                cmd = new SqlCommand(
                    "SELECT v.ID, p.Producto, v.Cantidad, v.Precio, v.Ganancia, v.Fecha " +
                    "FROM Productos p JOIN Ventas v ON p.ID = v.ID " +
                    "WHERE v.Fecha = @Fecha " +
                    "ORDER BY v.TimeStampUltimaModificacion", cn);
                cmd.Parameters.AddWithValue("@Fecha", Fecha.ToString("yyyy-MM-dd"));
                SqlDataReader leer = cmd.ExecuteReader();
                var resultado = new List<VentasProductos>();
                while (leer.Read())
                {
                    var ventaProducto = new VentasProductos()
                    {
                        ID = leer.GetInt32(0),
                        Producto = leer.GetString(1),
                        Cantidad = leer.GetDecimal(2),
                        Precio = leer.GetDecimal(3),
                        Ganancia = leer.GetDecimal(4),
                        Fecha = leer.GetDateTime(5)
                    };
                    resultado.Add(ventaProducto);
                }
                return resultado;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al Buscar Compras: " + ex.ToString());
            }
            finally
            {
                CerrarConexion();
            }
        }
        public List<string> BuscarCategorias()
        {            
            try
            {
                var lista = new List<string>();
                AbrirConexion();
                cmd = new SqlCommand(string.Format("SELECT Categoria FROM Productos GROUP BY Categoria")
                    , cn);
                SqlDataReader leer = cmd.ExecuteReader();
                while (leer.Read())
                {
                    lista.Add(leer.GetString(0));
                }
                return lista;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error SQL al obtener el total de ventas.", ex);
            }
            finally
            {
                CerrarConexion();
            }            
        }
        public List<string> BuscarSubCategoria()
        {
            try
            {
                var lista = new List<string>();
                AbrirConexion();
                cmd = new SqlCommand(string.Format("SELECT Subcategoria FROM Productos group by Subcategoria")
                    , cn);
                SqlDataReader leer = cmd.ExecuteReader();
                while (leer.Read())
                {
                    lista.Add(leer.GetString(0));
                }
                return lista;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error SQL al obtener el total de ventas.", ex);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public List<RelacionCroquetaBulto> SelectRelacionCroquetaBultoIDCroqueta(int idCroqueta)
        {
            try
            {
                AbrirConexion();
                using var cmd = new SqlCommand(@"
                SELECT IDCroqueta, IDBulto, KilosBultos
                  FROM RelacionCroquetaBulto
                 WHERE IDCroqueta = @IDCroqueta
              ORDER BY KilosBultos DESC", cn);
                cmd.Parameters.AddWithValue("@IDCroqueta", idCroqueta);

                using var reader = cmd.ExecuteReader();
                var resultados = new List<RelacionCroquetaBulto>();

                while (reader.Read())
                {
                    resultados.Add(new RelacionCroquetaBulto
                    {
                        IDCroqueta = reader.GetInt32(0),
                        IDBulto = reader.GetInt32(1),
                        KilosBultos = reader.GetDecimal(2)
                    });
                }
                return resultados;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(
                    $"Error SQL al obtener relaciones croqueta vs Bultos con el idCroquetas={idCroqueta}: {ex.Message}", ex);
            }
            finally
            {
                CerrarConexion();
            }

        }
        public void SelectProductoId(int idProducto)
        {
            {
                try
                {
                    AbrirConexion();

                    using var cmd = new SqlCommand(@"
            SELECT 1
              FROM Productos
             WHERE ID = @ID", cn);
                    cmd.Parameters.AddWithValue("@ID", idProducto);

                    using var reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException(
                            $"No existe producto con ID={idProducto}.");
                    }
                }
                catch (SqlException ex)
                {
                    throw new InvalidOperationException(
                        $"Error SQL al consultar producto ID={idProducto}: {ex.Message}", ex);
                }
                finally
                {
                    CerrarConexion();
                }
            }
        }
        public Productos SelectProductoId2(int idProducto)
        {
            {
                try
                {
                    AbrirConexion();

                    using var cmd = new SqlCommand(@"
            SELECT Categoria, Subcategoria, Producto, TimeStampAlta
              FROM Productos
             WHERE ID = @ID", cn);
                    cmd.Parameters.AddWithValue("@ID", idProducto);

                    using var reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException(
                            $"No existe producto con ID={idProducto}.");
                    }
                    var producto = new Productos
                    {
                        ID = idProducto,
                        Categoria = reader.GetString(0),
                        Subcategoria = reader.GetString(1),
                        Producto = reader.GetString(2),
                        TimeStampAlta = reader.GetDateTime(3),
                    };
                    return producto;

                }
                catch (SqlException ex)
                {
                    throw new InvalidOperationException(
                        $"Error SQL al consultar producto ID={idProducto}: {ex.Message}", ex);
                }
                finally
                {
                    CerrarConexion();
                }
            }
        }

    }
}
