using WApiPosExpress.Datos;

namespace WApiPosExpress.Models
{
    public class CategoriaRequest
    {
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public string? Activo { get; set; }
        public int? IdCategoriaPadre { get; set; }
    }
}
