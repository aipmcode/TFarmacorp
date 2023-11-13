using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using WApiPosExpress.Datos.Entidades;
using WApiPosExpress.Models;
using WApiPosExpress.Servicios.Interface;
using WApiPosExpress.Servicios.Servicio;

namespace WApiPosExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServicio _servicio;
        public CategoriaController( ICategoriaServicio serv)
        {
            _servicio = serv;
        }
        [HttpGet(Name = "GetCategorias")]
        public async Task<IActionResult> GetCategorias()
        {
           var resultado = await _servicio.ObtenerCategorias();
            if(resultado==null)
                return BadRequest("Ha ocurrido un error al obtener la información.");

            return Ok(resultado);
        }
        [HttpPost("RegistrarCategoriaProducto")]
        public async Task<IActionResult> RegistrarCategoriaProducto(AsignacionCategoriaRequest asignacionCategoria)
        {
            var resultado = await _servicio.RegistroCategoriaProducto(asignacionCategoria);

            if (!resultado.esCorrecto)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
        [HttpGet("GetProdcutoCategorias")]
        public async Task<IActionResult> GetProdcutoCategorias(int idProducto)
        {
            var resultado = await _servicio.ObtenerProductoCategoriasPorProdcuto(idProducto);
            if (resultado == null)
                return BadRequest("Ha ocurrido un error al obtener la información.");

            return Ok(resultado);
        }
    }
}
