using MASCOSHOP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASCOSHOP.Business
{
    internal class GestorCompras
    {
        private readonly ConexionDB conexion;
        public GestorCompras()
        {
            conexion = new ConexionDB();
        }
        public Compras ObtenerTotalCompras()
        {
            return conexion.SelectTotalCompras();
        }
        public void AgregarNuevaCompra(string descripcion, decimal importe)
        {
            var nuevaCompra = new Compras
            {
                ID = conexion.SelectIdMaxCompras() + 1,
                Descripcion = descripcion,
                Precio = importe,
                Fecha = DateTime.Now
            };
            conexion.InsertCompra(nuevaCompra);
        }
        public List<Compras> ObtenerCompras()
        {
            return conexion.SelectCompras();
        }
    }
}
