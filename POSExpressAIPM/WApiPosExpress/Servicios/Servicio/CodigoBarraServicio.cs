using WApiPosExpress.Datos.Entidades;
using WApiPosExpress.Datos;
using WApiPosExpress.Models;
using WApiPosExpress.Servicios.Interface;

namespace WApiPosExpress.Servicios.Servicio
{
    public class CodigoBarraServicio:ICodigoBarraServicio
    {
        public async Task<Respuesta> AsigmarCodigoBarra(CodigoBarraRequest codigoBarra)
        {
            Respuesta respuesta = new();
            try
            {     
                CodigosBarras codigoBarraProducto = new CodigosBarras()
                {
                   IdProducto = codigoBarra.IdProducto,
                   UniqueCodigo = codigoBarra.UniqueCodigo,
                   Activo= codigoBarra.Activo
                };
              
                OperacionesProducto op = new OperacionesProducto();
                int result = await op.RegistrarCodigoBarra(codigoBarraProducto);
                if (result > 0)
                {
                    respuesta.esCorrecto = true;
                    respuesta.mensaje = Constantes.MENSAJE_OK;
                    respuesta.datos = result;
                }

            }
            catch (Exception)
            {
                respuesta.mensaje = "Ha ocurrido una excepción.";
            }
            return respuesta;
        }
       
    }
}
