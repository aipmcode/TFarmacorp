using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WApiPosExpress.Datos.Entidades
{
    public class ProductosCategorias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetalle { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }

        [ForeignKey(nameof(IdProducto))]
        public virtual ExpProductos? Producto { get; set; }

        [ForeignKey(nameof(IdCategoria))]
        public virtual Categorias? Categoria { get; set; }

    }
}
