using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WApiPosExpress.Datos.Entidades
{
    public class CodigosBarras
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCodigoBarra { get; set; }
        [Required]
        public string UniqueCodigo { get; set; }
        public string Activo { get; set; }
        public int IdProducto { get; set; }
        [ForeignKey(nameof(IdProducto))]
        public virtual ExpProductos? Producto { get; set; }
    }
}
