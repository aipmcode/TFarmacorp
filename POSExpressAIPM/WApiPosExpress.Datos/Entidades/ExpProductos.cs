using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WApiPosExpress.Datos.Entidades
{
    public class ExpProductos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Activo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string? Observaciones { get; set; }
        public int IdTipoProducto { get; set; }
        [ForeignKey(nameof(IdTipoProducto))]
        public virtual TiposProducto? TiposProducto { get; set; }

        public virtual ErpProductos? ErProductos { get; set; }
        public virtual ICollection<CodigosBarras>? CodBarras { get; set; }
        public virtual ICollection<ProductosCategorias>? ProdCategorias { get; set; }
        public virtual ICollection<VentaExpress>? Ventas { get; set; }
    }
}
