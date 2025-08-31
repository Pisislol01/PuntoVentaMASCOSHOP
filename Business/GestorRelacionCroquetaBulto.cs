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
        public void AgregarRelacionCroquetasBulto(int iDCroqueta, int idBulto, decimal kilosBulto)
        {
            if (iDCroqueta == idBulto)
            {
                throw new InvalidOperationException("No se puede relacionar un producto con sí mismo.");
            }
            var relacion = new RelacionCroquetaBulto
            {
                IDCroqueta = iDCroqueta,
                IDBulto = idBulto,
                KilosBultos = kilosBulto
            };
            if (conexion.SelectRelacionCroquetaBulto(relacion) >= 1)
            {
                throw new InvalidOperationException("La relación de croquetas vs bulto ya existe.");
            }
            conexion.InsertRelacionCroquetaBulto(relacion);
        }
        public List<RelacionCroquetaBultoDTO> ObtenerTotalRelaciones()
        {
            return conexion.SelectTotalRelacionCroquetaBulto();
        }
    }
}
