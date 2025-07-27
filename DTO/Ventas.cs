using System;
using System.Collections.Generic;
using System.Text;

namespace MASCOSHOP.DTO
{
    class Ventas
    {
        public int ID { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Ganancia { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime TimeStampAlta { get; set; }
        public DateTime TimeStampUltimaModificacion { get; set; }
    }
    class VentasProductos
    {
        public int ID { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Ganancia { get; set; }
        public DateTime Fecha { get; set; }
    }
}
