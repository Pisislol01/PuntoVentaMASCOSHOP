using MASCOSHOP.DTO;
using MASCOSHOP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASCOSHOP.Business
{
    internal class GestorPrecios
    {
        private readonly ConexionDB conexion;
        public GestorPrecios()
        {
            conexion = new ConexionDB();
        }
        public Precios ObtenerPrecios(int idProducto)
        {
            return conexion.SelectPreciosID2(idProducto);
        }
        public decimal CalcularPrecio(decimal precio, decimal cantidadOPrecio, ModoVenta modo)
        {
            return modo switch
            {
                ModoVenta.Cantidad => precio * cantidadOPrecio,
                ModoVenta.Precio => cantidadOPrecio,
                _ => precio
            };
        }
        public decimal CalcularCantidadDesdePrecio(decimal precio, decimal precioVenta)
        {
            return precio / precioVenta;
        }
        public void AgregarPrecioNuevo(Precios precios)
        {
            conexion.InsertPrecios(precios);
        }
        public void ActualizarPreciosID(Precios precios)
        {
            conexion.UpdatePrecios(precios);
        }
    }
}
