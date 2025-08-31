using System;
using System.Collections.Generic;
using System.Text;

namespace MASCOSHOP.DTO
{
    internal class RelacionCroquetaBulto
    {
        public int IDCroqueta { get; set; }
        public int IDBulto { get; set; }
        public decimal KilosBultos { get; set; }
        public DateTime TimeStampAlta { get; set; }
        public DateTime TimeStampUltimaModificacion { get; set; }
    }
    internal class RelacionCroquetaBultoDTO
    {
        public int IDCroqueta { get; set; }
        public string NombreCroqueta { get; set; }
        public int IDBulto { get; set; }
        public string NombreBulto { get; set; }
        public decimal KilosBultos { get; set; }
    }
}
