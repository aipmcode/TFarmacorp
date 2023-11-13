using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WApiPosExpress.Datos.Entidades;
using WApiPosExpress.Models;
using WApiPosExpress.Servicios.Interface;

namespace WApiPosExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaExpressServicio _ventaExpressServicio;

        public VentaController(IVentaExpressServicio ventaExpressServicio)
        {
            _ventaExpressServicio = ventaExpressServicio;
        }

        [HttpPost("RealizarVenta")]
        public async Task<IActionResult> RealizarVenta([FromBody] VentaRequest venta)
        {
            var resultado = await _ventaExpressServicio.RegistroVenta(venta);

            if (!resultado.esCorrecto)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
