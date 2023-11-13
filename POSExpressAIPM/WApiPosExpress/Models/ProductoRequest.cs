using WApiPosExpress.Datos.Entidades;

namespace WApiPosExpress.Models
{
    public class ProductoRequest
    {
        public int IdProducto { get; set; }
        public double Costo { get; set; }
        public string UniqueCodigo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Stock { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Activo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string? Observaciones { get; set; }
        public TiposProducto tipoProducto { get; set; }
    }
}
