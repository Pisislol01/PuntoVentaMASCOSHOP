using MASCOSHOP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASCOSHOP.Business
{
    internal class GestorAjustes
    {

        private readonly ConexionDB conexion;
        public GestorAjustes()
        {
            conexion = new ConexionDB();
        }

        public Ajustes ObtenerAjustesPorFecha(DateTime fecha)
        {
            return conexion.SelectAjustesPorFecha(fecha);

        }
        public Ajustes ObtenerTotalAjustes()
        {
            return conexion.SelectTotalAjustes();
        }
        public void AgregarAjuste(string descripcion, decimal precioVenta, decimal precioGanancia)
        {
            var nuevoAjuste = new Ajustes()
            {
                ID = conexion.SelectIdMaxAjuste() + 1,
                Descripcion = descripcion,
                Precio_venta = precioVenta,
                Precio_ganancia = precioGanancia,
                Fecha = DateTime.Now
            };
            conexion.InsertAjuste(nuevoAjuste);
        }
        public List<Ajustes> ObtenerAjustes()
        {
            return conexion.ObtenerAjustes();
        }
    }

}
