using Microsoft.AspNetCore.Mvc;
using ProductosApi.DTOs;
using ProductosApi.Services;

namespace ProductosApi.Controllers;

[ApiController]
[Route("api/productos")]
public sealed class ProductosController(IProductoService productoService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductoDto>), StatusCodes.Status200OK, "application/json")]
    public IActionResult GetAll()
    {
        return Ok(productoService.GetAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var producto = productoService.GetById(id);
        return producto is null ? NotFound() : Ok(producto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status201Created, "application/json")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    public IActionResult Create([FromBody] CreateProductoDto productoDto)
    {
        var producto = productoService.Create(productoDto);
        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] CreateProductoDto productoDto)
    {
        var producto = productoService.Update(id, productoDto);
        return producto is null ? NotFound() : Ok(producto);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        return productoService.Delete(id) ? NoContent() : NotFound();
    }
}
