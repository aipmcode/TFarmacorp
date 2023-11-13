using WApiPosExpress.Datos;
using WApiPosExpress.Datos.Entidades;
using WApiPosExpress.Models;
using WApiPosExpress.Servicios.Interface;

namespace WApiPosExpress.Servicios.Servicio
{
    public class ProductoServicio : IProductoServicio
    {
        public async Task<Respuesta> ObtenerProductos()
        {
            Respuesta respuesta = new();
            try
            {
                OperacionesProducto op = new OperacionesProducto();
                var ListaExpProd = await op.ObtenerExpProductos();
                var ListaErpProd = await op.ObtenerErpProductos();
                if (ListaExpProd != null && ListaExpProd.Count>0)
                {
                    var resultado = from expProducto in ListaExpProd
                                    join erpProducto in ListaErpProd
                                    on expProducto.IdProducto equals erpProducto.IdProducto
                                    select new ProductoRequest
                                    {
                                        IdProducto = expProducto.IdProducto,
                                        Costo = erpProducto.Costo,
                                        UniqueCodigo = erpProducto.UniqueCodigo,
                                        FechaRegistro = erpProducto.FechaRegistro,
                                        Stock = erpProducto.Stock,
                                        Nombre = expProducto.Nombre,
                                        Precio = expProducto.Precio,
                                        Activo = expProducto.Activo,
                                        FechaVencimiento = expProducto.FechaVencimiento,
                                        Observaciones = expProducto.Observaciones,
                                        tipoProducto = new TiposProducto {IdTipoProducto= expProducto.IdTipoProducto } 
                                    };

                    var listaResultado = resultado.ToList();

                    respuesta.esCorrecto = true;
                    respuesta.mensaje = Constantes.MENSAJE_OK;
                    respuesta.datos = listaResultado;
                }

            }
            catch (Exception)
            {
                respuesta.mensaje = "Ha ocurrido una excepción.";
            }
            return respuesta;
        }

        public async Task<List<TiposProducto>> ObtenerTiposProducto()
        {
            List<TiposProducto> lista = new();
            OperacionesProducto op = new OperacionesProducto();
            try
            {
                await op.insertarTipoProductodDummy();
                lista = await op.ObtenerTodosTiposProducto();               
            }
            catch (Exception)
            {
            }
            return lista;
        }

        public async Task<Respuesta> RegistroProducto(ProductoRequest producto)
        {
            Respuesta respuesta = new();
            try
            {
                respuesta = await ProcesarInsertProducto(producto);            

            }
            catch (Exception)
            {
                respuesta.mensaje = "Ha ocurrido una excepción.";
            }
            return respuesta;
        }
        private async Task<Respuesta> ProcesarInsertProducto(ProductoRequest producto)
        {
            Respuesta respuesta = new();
            ExpProductos expProd = new ExpProductos() { 
                Nombre= producto.Nombre,
                Precio= producto.Precio,
                Activo= producto.Activo,
                FechaVencimiento = producto.FechaVencimiento,
                Observaciones= producto.Observaciones,
                IdTipoProducto= producto.tipoProducto.IdTipoProducto
            };
            ErpProductos erpProd= new ErpProductos() { 
                Costo= producto.Costo,
                UniqueCodigo= producto.UniqueCodigo,
                FechaRegistro= DateTime.Now,
                Stock=producto.Stock
            };
            OperacionesProducto op = new OperacionesProducto();
            int result = await op.RegistrarProducto(expProd, erpProd);
            if (result>0)
            {
                respuesta.esCorrecto = true;
                respuesta.mensaje = Constantes.MENSAJE_OK;
                respuesta.datos = result;
            }
            return respuesta;

        }
    }
}
