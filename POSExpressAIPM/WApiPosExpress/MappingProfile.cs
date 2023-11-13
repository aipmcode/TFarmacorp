using AutoMapper;
using WApiPosExpress.Datos.Entidades;
using WApiPosExpress.Models;

namespace WApiPosExpress
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Categorias , CategoriaRequest>();
            CreateMap<VentaRequest, VentaExpress>();
        }
    }
}
