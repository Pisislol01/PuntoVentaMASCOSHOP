using System;
using System.Collections.Generic;
using System.Text;

namespace MASCOSHOP
{
    class Productos
    {
        public int ID{ get; set; }
        public String Categoria { get; set; }
        public String Subcategoria { get; set; }
        public String Producto { get; set; }
        public DateTime TimeStampAlta { get; set; }
        public DateTime TimeStampUltimaModificacion { get; set; }
    }
}
