using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WApiPosExpress.Models;

namespace WApiPosExpress.Servicios.Interface
{
    public interface ICategoriaServicio
    {
        public Task<Respuesta> ObtenerCategorias();
        public Task<Respuesta> RegistroCategoriaProducto(AsignacionCategoriaRequest asignacionCategoria);
        public Task<Respuesta> ObtenerProductoCategoriasPorProdcuto(int idProducto);
    }
}
