using AutoMapper;
using WApiPosExpress.Datos;
using WApiPosExpress.Datos.Entidades;
using WApiPosExpress.Models;
using WApiPosExpress.Servicios.Interface;

namespace WApiPosExpress.Servicios.Servicio
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly IMapper _mapper;
        public CategoriaServicio(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<Respuesta> ObtenerCategorias()
        {
            Respuesta respuesta = new();            
            try
            {
                OperacionesCategoria op = new OperacionesCategoria();
                var categoriasDesdeBD = await op.ObtenerTodasCategorias();
                if(categoriasDesdeBD!= null)
                {
                    List<CategoriaRequest> categoriasLista = new List<CategoriaRequest>();
                    categoriasLista = _mapper.Map<List<CategoriaRequest>>(categoriasDesdeBD);

                    respuesta.esCorrecto = true;
                    respuesta.mensaje = Constantes.MENSAJE_OK;
                    respuesta.datos= categoriasLista;
                }
                
            }
            catch (Exception)
            {
                respuesta.mensaje = "Ha ocurrido una excepción.";
            }
            return respuesta;
        }
             
        public async Task<Respuesta> ObtenerProductoCategoriasPorProdcuto(int idProducto)
        {

            Respuesta respuesta = new();
            try
            {
                OperacionesCategoria op = new OperacionesCategoria();
                var categoriasDesdeBD = await op.ObtenerCategoriasProductoPorIdProducto(idProducto);
                if (categoriasDesdeBD != null)
                {
                    List<ProductosCategorias> categoriasLista = new List<ProductosCategorias>();
                    categoriasLista = _mapper.Map<List<ProductosCategorias>>(categoriasDesdeBD);

                    respuesta.esCorrecto = true;
                    respuesta.mensaje = Constantes.MENSAJE_OK;
                    respuesta.datos = categoriasLista;
                }

            }
            catch (Exception)
            {
                respuesta.mensaje = "Ha ocurrido una excepción.";
            }
            return respuesta;
        }

        public async Task<Respuesta> RegistroCategoriaProducto(AsignacionCategoriaRequest asignacionCategoria)
        {
            Respuesta respuesta = new();
            try
            {
                OperacionesCategoria op = new OperacionesCategoria();
                ProductosCategorias prodCategoria = new ProductosCategorias {
                    FechaCreacion= DateTime.Now,
                    IdCategoria= asignacionCategoria.idCategoria,
                    IdProducto= asignacionCategoria.idProducto
                };
                int id_detalle = await op.RegistrarProductoCategoria(prodCategoria);
                if (id_detalle >0)
                {
                    respuesta.esCorrecto = true;
                    respuesta.mensaje = Constantes.MENSAJE_OK;
                    respuesta.datos = id_detalle;
                }

            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Error al registrar la categoría. Detalles: " + ex.Message;
            }
            return respuesta;
        }
    }
}
