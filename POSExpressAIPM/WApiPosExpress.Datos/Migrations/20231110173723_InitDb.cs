using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WApiPosExpress.Datos.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCategoriaPadre = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_IdCategoriaPadre",
                        column: x => x.IdCategoriaPadre,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria");
                });

            migrationBuilder.CreateTable(
                name: "TiposProducto",
                columns: table => new
                {
                    IdTipoProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProducto", x => x.IdTipoProducto);
                });

            migrationBuilder.CreateTable(
                name: "ExpProductos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTipoProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpProductos", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_ExpProductos_TiposProducto_IdTipoProducto",
                        column: x => x.IdTipoProducto,
                        principalTable: "TiposProducto",
                        principalColumn: "IdTipoProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodigosBarras",
                columns: table => new
                {
                    IdCodigoBarra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueCodigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosBarras", x => x.IdCodigoBarra);
                    table.ForeignKey(
                        name: "FK_CodigosBarras_ExpProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ExpProductos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErpProductos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false),
                    UniqueCodigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpProductos", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_ErpProductos_ExpProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ExpProductos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductosCategorias",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosCategorias", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_ProductosCategorias_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosCategorias_ExpProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ExpProductos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentaExpress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Descuento = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaExpress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentaExpress_ExpProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ExpProductos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_IdCategoriaPadre",
                table: "Categorias",
                column: "IdCategoriaPadre");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosBarras_IdProducto",
                table: "CodigosBarras",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ExpProductos_IdTipoProducto",
                table: "ExpProductos",
                column: "IdTipoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCategorias_IdCategoria",
                table: "ProductosCategorias",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCategorias_IdProducto",
                table: "ProductosCategorias",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_VentaExpress_IdProducto",
                table: "VentaExpress",
                column: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodigosBarras");

            migrationBuilder.DropTable(
                name: "ErpProductos");

            migrationBuilder.DropTable(
                name: "ProductosCategorias");

            migrationBuilder.DropTable(
                name: "VentaExpress");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "ExpProductos");

            migrationBuilder.DropTable(
                name: "TiposProducto");
        }
    }
}
