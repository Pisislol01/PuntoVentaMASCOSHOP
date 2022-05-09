using System;
using System.Collections.Generic;
using System.Text;

namespace MASCOSHOP
{
    class Inventario
    {
        public int ID { get; set; }
        public decimal CompraIni { get; set; }
        public decimal Compra { get; set; }
        public decimal Venta { get; set; }
        public decimal Existencia { get; set; }
        public DateTime TimeStampAlta { get; set; }
        public DateTime TimeStampUltimaModificacion { get; set; }
    }
}
