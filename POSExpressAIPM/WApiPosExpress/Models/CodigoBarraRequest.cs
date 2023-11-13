using System.ComponentModel.DataAnnotations;

namespace WApiPosExpress.Models
{
    public class CodigoBarraRequest
    {
        public int IdCodigoBarra { get; set; }
        public string UniqueCodigo { get; set; }
        public string Activo { get; set; }
        public int IdProducto { get; set; }
    }
}
