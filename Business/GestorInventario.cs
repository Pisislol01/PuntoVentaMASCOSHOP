using MASCOSHOP.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MASCOSHOP.Business
{
    internal class GestorInventario
    {
        private readonly ConexionDB conexion;
        public GestorInventario()
        {
            conexion = new ConexionDB();
        }
        public Inventario AsegurarInventarioDisponible(int idProducto, decimal cantidad)
        {
            var inventario = conexion.SelectInventarioID2(idProducto);
            if (inventario.Existencia < cantidad)
            {
                var gestorRelacionCroquetaBulto = new GestorRelacionCroquetaBulto();
                gestorRelacionCroquetaBulto.ConvertirBultoACroquetas(inventario);
                if (inventario.Existencia < cantidad)
                {
                    throw new InvalidOperationException($"Hay menos productos en el almacen ({inventario.Existencia}) " +
                    $"que los que intenta vender ({cantidad})");
                }
            }
            return inventario;
        }
        public void ActualizarInventarioVenta(Inventario inventario, decimal cantidad)
        {
            inventario.Venta += cantidad;
            inventario.Existencia -= cantidad;
            conexion.UpddateInventarioID2(inventario);
        }
        public void AgregarProductoInventario(int idproducto, decimal cantidad)
        {
            var inventario = conexion.SelectInventarioID2(idproducto);
            inventario.Compra += cantidad;
            inventario.Existencia += cantidad;
            conexion.UpddateInventarioID2(inventario);

        }
        public void ActualizarInventarioCancelarVenta(int idProducto, decimal cantidadTotal)
        {
            var inventario = conexion.SelectInventarioID2(idProducto);
            inventario.Venta -= cantidadTotal;
            inventario.Existencia += cantidadTotal;
            conexion.UpddateInventarioID2(inventario);
        }
        public void AgregarInventarioNuevo(int idProducto, int numeroProductosComprados)
        {
            conexion.InsertInventario(idProducto, numeroProductosComprados);
        }
    }
}
