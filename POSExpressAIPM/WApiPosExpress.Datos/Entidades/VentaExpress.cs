using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WApiPosExpress.Datos.Entidades
{
    public class VentaExpress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public string UniqueProducto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Descuento { get; set; }
        public double Total { get; set; }
        public int IdProducto { get; set; }

        [ForeignKey(nameof(IdProducto))]
        public virtual ExpProductos? ExpProducto { get; set; }
    }
}
