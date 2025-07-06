using System;
using System.Collections.Generic;
using System.Text;

namespace MASCOSHOP.DTO
{
    class Compras
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime TimeStampAlta { get; set; }
        public DateTime TimeStampUltimaModificacion { get; set; }
    }
}
