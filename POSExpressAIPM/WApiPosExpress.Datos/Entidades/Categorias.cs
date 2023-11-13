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
    public class Categorias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public string? Activo { get; set; }
        public int? IdCategoriaPadre { get; set; }
        [ForeignKey("IdCategoriaPadre")]
        public virtual Categorias? CategoriaPadre { get; set; }

        public virtual ICollection<ProductosCategorias>? ProdCategorias { get; set; }
    }
}
