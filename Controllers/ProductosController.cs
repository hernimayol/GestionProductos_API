using Microsoft.AspNetCore.Mvc;
using ProductosApi.DTOs;
using ProductosApi.Services;

namespace ProductosApi.Controllers;

[ApiController]
[Route("api/productos")]
public sealed class ProductosController(IProductoService productoService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<ProductoDto>), StatusCodes.Status200OK, "application/json")]
    public IActionResult ObtenerTodos([FromQuery] string? categoria)
    {
        var productos = string.IsNullOrWhiteSpace(categoria)
            ? productoService.ObtenerTodos()
            : productoService.BuscarPorCategoria(categoria.Trim());

        return Ok(productos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public IActionResult ObtenerPorId(int id)
    {
        var producto = productoService.ObtenerPorId(id);
        return producto is null ? NotFound() : Ok(producto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status201Created, "application/json")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    public IActionResult Agregar([FromBody] CreateProductoDto productoDto)
    {
        var producto = productoService.Agregar(productoDto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public IActionResult Actualizar(int id, [FromBody] CreateProductoDto productoDto)
    {
        var producto = productoService.Actualizar(id, productoDto);
        return producto is null ? NotFound() : Ok(producto);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public IActionResult Eliminar(int id)
    {
        return productoService.Eliminar(id) ? NoContent() : NotFound();
    }
}
