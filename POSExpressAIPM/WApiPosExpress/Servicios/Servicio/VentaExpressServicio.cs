using WApiPosExpress.Datos.Entidades;
using WApiPosExpress.Datos;
using WApiPosExpress.Models;
using WApiPosExpress.Servicios.Interface;
using AutoMapper;

namespace WApiPosExpress.Servicios.Servicio
{
    public class VentaExpressServicio : IVentaExpressServicio
    {
        private readonly IMapper _mapper;
        public VentaExpressServicio(IMapper mapper1)
        {
            _mapper = mapper1;
        }
        public async Task<Respuesta> RegistroVenta(VentaRequest venta)
        {
            Respuesta respuesta = new();
            try
            {
                VentaExpress ventaExpress= _mapper.Map<VentaExpress>(venta);
                OperacionesVenta op = new OperacionesVenta();
                int result = await op.RegistrarVenta(ventaExpress);
                if (result > 0)
                {
                    //ACTUALIZAR STOCK DE PRODUCTOS
                    OperacionesProducto operacionesProducto = new OperacionesProducto();
                    int resulrAct = await operacionesProducto.ActualizarStockProducto(ventaExpress.IdProducto, ventaExpress.Cantidad);
                    if (resulrAct > 0)
                    {
                        respuesta.esCorrecto = true;
                        respuesta.mensaje = Constantes.MENSAJE_OK;
                        respuesta.datos = result;
                    }
                        
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
