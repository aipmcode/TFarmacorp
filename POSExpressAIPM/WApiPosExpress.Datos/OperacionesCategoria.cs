using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WApiPosExpress.Datos.Entidades;

namespace WApiPosExpress.Datos
{
    public class OperacionesCategoria
    {
        private readonly DbExpressContext _context;
        public OperacionesCategoria(DbExpressContext context = null)
        {
            _context = context ?? DbContextFactory.CreateContext();
        }

        public async Task<List<Categorias>> ObtenerTodasCategorias()
        {
            List<Categorias> lita= new List<Categorias>();
            lita= await _context.Categorias.ToListAsync();
            return lita;
        }
        public async Task<int> RegistrarProductoCategoria(ProductosCategorias datos)
        {
            int result=0;
            _context.ProductosCategorias.Add(datos);
            await _context.SaveChangesAsync();
            result = datos.IdDetalle;
            return result;
           
        }
        public async Task<List<ProductosCategorias>> ObtenerCategoriasProductoPorIdProducto(int idProducto)
        {
            List<ProductosCategorias> lita = new List<ProductosCategorias>();
            lita = await _context.ProductosCategorias.Where(x=> x.IdProducto==idProducto).ToListAsync();
            return lita;
        }
    }
}
