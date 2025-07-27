using MASCOSHOP.DTO;
using MASCOSHOP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASCOSHOP.Business
{
    internal class GestorVentas
    {
        private readonly ConexionDB conexion;
        public GestorVentas()
        {
            conexion = new ConexionDB();
        }
        public void CrearVenta(Precios precios, decimal cantidad)
        {
            Ventas Ventas = new Ventas()
            {
                ID = precios.ID,
                Cantidad = cantidad,
                Precio = cantidad * precios.PrecioVenta,
                Ganancia = cantidad * (precios.PrecioVenta - precios.PrecioCompra),
                Fecha = DateTime.Today
            };
            conexion.InsertVenta(Ventas);
        }
        public void CrearVentaFecha(Precios precios, decimal cantidad, DateTime fecha)
        {
            Ventas Ventas = new Ventas()
            {
                ID = precios.ID,
                Cantidad = cantidad,
                Precio = cantidad * precios.PrecioVenta,
                Ganancia = cantidad * (precios.PrecioVenta - precios.PrecioCompra),
                Fecha = fecha
            };
            conexion.InsertVenta(Ventas);
        }
        public Ventas ObtenerTotalVentasCancelar(int idProducto, decimal cantidad)
        {
            Ventas ventas = new Ventas()
            {
                ID = idProducto,
                Cantidad = cantidad,
                Fecha = DateTime.Today
            };
            conexion.ObtenerTotalVentasCancelar(ventas);
            return ventas;
        }
        public void EliminarVentasPorIdCantidadFecha(int idProducto, decimal cantidad)
        {
            Ventas ventas = new Ventas()
            {
                ID = idProducto,
                Cantidad = cantidad,
                Fecha = DateTime.Today
            };
            conexion.DeleteVentasPorIdCantidadFecha(ventas);

        }
        public Ventas ObtenerVentasPorFecha(DateTime fecha)
        {
            return conexion.SelectVentasTotalFecha(fecha);

        }
        public decimal ObtenerGananciaTotal(Ventas venta, Ajustes ajuste)
        {
            return (venta.Ganancia) + (ajuste.Precio_ganancia);
        }
        public Ventas ObtenerTotalVentas()
        {
            return conexion.SelectTotalVentas();
        }
        public decimal ObtenerEfectivoReal(Ventas ventas, Compras compras, Ajustes ajustes)
        {
            return (ventas.Precio + ajustes.Precio_venta - compras.Precio);
        }
        public List<VentasProductos> ObtenerVentasPorFecha2(DateTime fecha)
        {
            return conexion.SelectVentasPorFecha(fecha);
        }
    }
}
