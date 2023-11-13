using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSExpress.Presentacion
{
    internal class ConstantesServicio
    {
        internal const string url = "https://localhost:7150";
        internal const string ObtenerCategorias = "/api/Categoria";
        internal const string ObtenerTiposProductos = "/api/Producto/ObtenerTipoProducto";
        internal const string RegistrarProducto = "/api/Producto/RegistrarProducto";
        internal const string ObtenerTodosProductos = "/api/Producto";
        internal const string ObtenerProductosCategorias = "/api/Categoria/GetProdcutoCategorias";
        
        internal const string RegistrarCodigoBarrar = "/api/CodigoBarra/AsignarCodigoBarras";
        internal const string AsignarCategoria = "/api/Categoria/RegistrarCategoriaProducto";
        internal const string RegistrarVenta = "/api/Venta/RealizarVenta";
    }
}
