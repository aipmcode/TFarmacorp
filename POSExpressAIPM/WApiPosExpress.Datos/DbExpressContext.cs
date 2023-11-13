using Microsoft.EntityFrameworkCore;
using WApiPosExpress.Datos.Entidades;

namespace WApiPosExpress.Datos
{
    public class DbExpressContext: DbContext
    {
        public DbExpressContext(DbContextOptions<DbExpressContext>options):base(options)
        {

        }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<CodigosBarras> CodigosBarras { get; set; }
        public DbSet<ErpProductos> ErpProductos { get; set; }
        public DbSet<ExpProductos> ExpProductos { get; set; }
        public DbSet<ProductosCategorias> ProductosCategorias { get; set; }
        public DbSet<TiposProducto> TiposProducto { get; set; }
        public DbSet<VentaExpress> VentaExpress { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación entre Categorias y ProductosCategorias
            modelBuilder.Entity<ProductosCategorias>()
                .HasOne(pc => pc.Categoria)
                .WithMany(c => c.ProdCategorias)
                .HasForeignKey(pc => pc.IdCategoria);

            modelBuilder.Entity<ProductosCategorias>()
                .HasOne(pc => pc.Producto)
                .WithMany(p => p.ProdCategorias)
                .HasForeignKey(pc => pc.IdProducto);

            // Relación entre CodigosBarras y ErpProductos
            modelBuilder.Entity<CodigosBarras>()
                .HasOne(cb => cb.Producto)
                .WithMany(p => p.CodBarras)
                .HasForeignKey(cb => cb.IdProducto);

            // Relación entre ErpProductos y ExpProductos            
            modelBuilder.Entity<ErpProductos>()
           .HasOne(ep => ep.Producto)
           .WithOne(p => p.ErProductos)
           .HasForeignKey<ErpProductos>(ep => ep.IdProducto);



            // Relación entre ExpProductos y TiposProducto
            modelBuilder.Entity<ExpProductos>()
                .HasOne(p => p.TiposProducto)
                .WithMany(tp => tp.Producto)
                .HasForeignKey(p => p.IdTipoProducto);

            // Relación entre ExpProductos y VentaExpress
            modelBuilder.Entity<VentaExpress>()
                .HasOne(ve => ve.ExpProducto)
                .WithMany(p => p.Ventas)
                .HasForeignKey(ve => ve.IdProducto);
        }
    }
}