using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WApiPosExpress.Datos.Entidades
{
    public class ErpProductos
    {
        [Key]
        public int IdProducto { get; set; }
        public double Costo { get; set; }

        [Required]
        public string UniqueCodigo { get; set; }

        public DateTime FechaRegistro { get; set; }
        public int Stock { get; set; }

        [ForeignKey("IdProducto")]
        public virtual ExpProductos Producto { get; set; }
    }
}
