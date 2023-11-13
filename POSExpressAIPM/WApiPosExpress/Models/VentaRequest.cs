namespace WApiPosExpress.Models
{
    public class VentaRequest
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public string UniqueProducto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Descuento { get; set; }
        public double Total { get; set; }
        public int IdProducto { get; set; }
    }
}
