using WApiPosExpress.Models;

namespace WApiPosExpress.Servicios.Interface
{
    public interface IVentaExpressServicio
    {
        public Task<Respuesta> RegistroVenta(VentaRequest venta);
    }
}
