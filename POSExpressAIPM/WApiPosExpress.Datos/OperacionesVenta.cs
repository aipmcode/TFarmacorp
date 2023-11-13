using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WApiPosExpress.Datos.Entidades;

namespace WApiPosExpress.Datos
{
    public class OperacionesVenta
    {
        private readonly DbExpressContext _context;
        public OperacionesVenta(DbExpressContext context = null)
        {
            _context = context ?? DbContextFactory.CreateContext();
        }
        public async Task<int> RegistrarVenta(VentaExpress venta)
        {
            int result = 0;
            _context.VentaExpress.Add(venta);
            await _context.SaveChangesAsync();
            result = venta.Id;

            return result;

        }
    }
}
