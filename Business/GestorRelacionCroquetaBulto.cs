using MASCOSHOP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASCOSHOP.Business
{
    internal class GestorRelacionCroquetaBulto
    {
        private readonly ConexionDB conexion;
        public GestorRelacionCroquetaBulto()
        {
            conexion = new ConexionDB();
        }
        public void ConvertirBultoACroquetas(Inventario inventarioCroqueta)
        {
            var rcb = new List<RelacionCroquetaBulto>();
            rcb = conexion.SelectRelacionCroquetaBultoIDCroqueta(inventarioCroqueta.ID);
            if (rcb.Count > 0)
            {
                foreach (var relaciones in rcb)
                {                                        
                    var inventarioBulto = conexion.SelectInventarioID2(relaciones.IDBulto);
                    if (inventarioBulto.Existencia > 0)
                    {
                        inventarioBulto.Compra -= 1;
                        inventarioBulto.Existencia -= 1;
                        conexion.UpddateInventarioID(inventarioBulto);
                        inventarioCroqueta.Compra += relaciones.KilosBultos;
                        inventarioCroqueta.Existencia += relaciones.KilosBultos;
                        conexion.UpddateInventarioID(inventarioCroqueta);
                        return;
                    }
                }
            }
        }
    }
}
