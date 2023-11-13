using WApiPosExpress.Models;

namespace WApiPosExpress.Servicios.Interface
{
    public interface ICodigoBarraServicio
    {
        public Task<Respuesta> AsigmarCodigoBarra(CodigoBarraRequest codigoBarra);
    }
}
