using MASCOSHOP.DTO;
using MASCOSHOP.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MASCOSHOP.Business
{
    internal class GestorLotes
    {
        private readonly ConexionDB conexion;
        public GestorLotes()
        {
            conexion = new ConexionDB();
        }
        public void ProcesarArchivoEntrada(OpenFileDialog ofd )
        {
            string rutaArchivo = ofd.FileName;
            string[] lineas = File.ReadAllLines(rutaArchivo);

            List<string> resultado = new List<string>();
            resultado.Add("ID,Cantidad,Fecha,Comentario"); // encabezado del archivo log

            foreach (string linea in lineas.Skip(1)) // Saltamos encabezado
            {
                string comentario = "";
                string[] datos = linea.Split(',');
                try
                {
                    if (datos.Length < 3)
                    {
                        throw new InvalidOperationException($"Línea incompleta");
                    }

                    if (!int.TryParse(datos[0], out int id) ||
                        !decimal.TryParse(datos[1], out decimal cantidad) ||
                        !DateTime.TryParse(datos[2], out DateTime fecha))
                    {
                        throw new InvalidOperationException($"ID, Cantidad o Fecha inválida");
                    }

                    var gestorInv = new GestorInventario();
                    /*obtiene el inventario y si no hay suficiente cantidad, abre un bulto y 
                      valida si hay suficiente cantidad
                      en caso de que no haya suficiente cantidad, lanza una excepción*/
                    var inventario = gestorInv.AsegurarInventarioDisponible(id, cantidad);
                    var gestorPrecios = new GestorPrecios();                    
                    var precios = gestorPrecios.ObtenerPrecios(id);

                    var gestorVentas = new GestorVentas();
                    gestorVentas.CrearVentaFecha(precios, cantidad, fecha);

                    gestorInv.ActualizarInventarioVenta(inventario, cantidad);

                    comentario = "OK";

                }
                catch (InvalidOperationException ex)
                {
                    comentario = ex.Message;
                }
                catch (Exception ex)
                {
                    comentario = ex.Message;
                }
                finally
                {
                    resultado.Add($"{datos[0]},{datos[1]},{datos[2]},{comentario}");
                }

            }

            string rutaLog = Path.Combine(Path.GetDirectoryName(rutaArchivo), "resultado_importacion.csv");
            File.WriteAllLines(rutaLog, resultado);

            MessageBox.Show("Importación finalizada. Total procesadas: " + resultado.Count, "Proceso terminado");
        }
    }
}
