using WApiPosExpress.Datos.Entidades;
using WApiPosExpress.Models;

namespace WApiPosExpress.Servicios.Interface
{
    public interface IProductoServicio
    {
        public Task<List<TiposProducto>> ObtenerTiposProducto();
        public Task<Respuesta> RegistroProducto(ProductoRequest producto);
        public Task<Respuesta> ObtenerProductos();
    }
}
