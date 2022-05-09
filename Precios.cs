using System;
using System.Collections.Generic;
using System.Text;

namespace MASCOSHOP
{
    class Precios
    {
        public int ID { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public DateTime TimeStampAlta { get; set; }
        public DateTime TimeStampUltimaModificacion { get; set; }
    }
}
