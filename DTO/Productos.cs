using System;
using System.Collections.Generic;
using System.Text;

namespace MASCOSHOP.DTO
{
    class Productos
    {
        public int ID { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public string Producto { get; set; }
        public DateTime TimeStampAlta { get; set; }
        public DateTime TimeStampUltimaModificacion { get; set; }
    }
    public class ProductoDTO
    {
        public int ID { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public string Producto { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Existencia { get; set; }
    }
}
