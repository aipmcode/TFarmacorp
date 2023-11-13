using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WApiPosExpress.Datos.Entidades;
using WApiPosExpress.Models;
using WApiPosExpress.Servicios.Interface;

namespace WApiPosExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodigoBarraController : ControllerBase
    {
        private readonly ICodigoBarraServicio _servicio;

        public CodigoBarraController(ICodigoBarraServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPost("AsignarCodigoBarras")]
        public async Task<IActionResult> AsignarCodigoBarras([FromBody] CodigoBarraRequest codigoBarra)
        {
            var resultado = await _servicio.AsigmarCodigoBarra(codigoBarra);

            if (!resultado.esCorrecto)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
