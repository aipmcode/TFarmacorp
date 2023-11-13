using Microsoft.AspNetCore.Mvc;
using WApiPosExpress.Datos.Entidades;
using WApiPosExpress.Models;
using WApiPosExpress.Servicios.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WApiPosExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServicio _erpProductosServicio;

        public ProductoController(IProductoServicio erpProductosServicio)
        {
            _erpProductosServicio = erpProductosServicio;
        }

        [HttpPost("RegistrarProducto")]
        public async Task<IActionResult> RegistrarProducto([FromBody] ProductoRequest producto)
        {
            var resultado = await _erpProductosServicio.RegistroProducto(producto);

            if (!resultado.esCorrecto)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
        [HttpGet("ObtenerTipoProducto")]
        public async Task<IActionResult> ObtenerTipoProducto()
        {
            var resultado = await _erpProductosServicio.ObtenerTiposProducto();

            if (resultado==null)
            {
                return BadRequest("Ha Ocurrido un error al obtener la lista de tipos de producto");
            }

            return Ok(resultado);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _erpProductosServicio.ObtenerProductos();
            if (resultado == null)
                return BadRequest("Ha ocurrido un error al obtener la información.");

            return Ok(resultado);
        }

    }
}
