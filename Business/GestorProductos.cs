using MASCOSHOP.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MASCOSHOP.Business
{
    internal class GestorProductos
    {
        private readonly ConexionDB conexion;
        public GestorProductos()
        {
            conexion = new ConexionDB();
        }
        public void VerificarExistenciaProducto(int idProducto)
        {
            conexion.SelectProductoId(idProducto);
        }
        public Productos ObtenerProductoID(int idProducto)
        {
            return conexion.SelectProductoId2(idProducto);            
        }
        public List<ProductoDTO> RegresarProductoBuscarPorNombre (string buscarNombre)
        {
            return conexion.BuscarProductoNombre(buscarNombre);
        }
        public List<string> ObtenerCategorias()
        {
            return conexion.BuscarCategorias();
        }
        public List<string> BuscarSubCategoria()
        {
            return conexion.BuscarSubCategoria();
        }
        public int ObtenerIdNuevoProducto()
        {
            return conexion.ObtenerIdNuevoProducto();
        }
        public void AgregarProductoNuevo(Productos producto)
        {
            conexion.InsertProducto(producto);
        }
    }

}
