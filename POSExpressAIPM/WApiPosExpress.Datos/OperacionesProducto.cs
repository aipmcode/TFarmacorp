using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WApiPosExpress.Datos.Entidades;

namespace WApiPosExpress.Datos
{
    public class OperacionesProducto
    {
        private readonly DbExpressContext _context;
        public OperacionesProducto(DbExpressContext context = null)
        {
            _context = context ?? DbContextFactory.CreateContext();
        }
        public async Task<int> RegistrarProducto(ExpProductos expProd, ErpProductos ErpProd)
        {
            int result = 0;
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.ExpProductos.Add(expProd);
                await _context.SaveChangesAsync();
                ErpProd.IdProducto = expProd.IdProducto;
                _context.ErpProductos.Add(ErpProd);
                await _context.SaveChangesAsync();


                result = expProd.IdProducto;
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            return result;

        }
        public async Task insertarTipoProductodDummy()
        {
            //sino tiene datos, inserta dtos de prueba.
            List<TiposProducto> lita = new List<TiposProducto>();
            lita = await _context.TiposProducto.ToListAsync();
            if(lita ==null || lita.Count <1)
            {
                TiposProducto tiposProducto = new TiposProducto() { Descripcion="Medicamentos"};
                TiposProducto tiposProducto2 = new TiposProducto() { Descripcion = "Cosmeticos" };
                _context.TiposProducto.Add(tiposProducto);
                _context.TiposProducto.Add(tiposProducto2);
                await _context.SaveChangesAsync();
            }
            
        }
        public async Task<List<TiposProducto>> ObtenerTodosTiposProducto()
        {
            List<TiposProducto> lita = new List<TiposProducto>();
            lita = await _context.TiposProducto.ToListAsync();
            return lita;
        }
        public async Task<int> RegistrarCodigoBarra(CodigosBarras codigoBarra)
        {
            int result = 0;
            _context.CodigosBarras.Add(codigoBarra);
            await _context.SaveChangesAsync();
            result = codigoBarra.IdCodigoBarra;
            return result;

        }
        public async Task<List<ExpProductos>> ObtenerExpProductos()
        {
            List<ExpProductos> lita = new List<ExpProductos>();
            lita = await _context.ExpProductos.ToListAsync();
            return lita;
        }
        public async Task<List<ErpProductos>> ObtenerErpProductos()
        {
            List<ErpProductos> lita = new List<ErpProductos>();
            lita = await _context.ErpProductos.ToListAsync();
            return lita;
        }
        public async Task<int> ActualizarStockProducto(int idProducto,int cantidad)
        {
            int result = 0;
            var producto = await _context.ErpProductos.FindAsync(idProducto);
            if (producto == null)
            {
                return 0;
            }
            producto.Stock = producto.Stock - cantidad;
            result= await _context.SaveChangesAsync();
            return result;
        }
    }
}
