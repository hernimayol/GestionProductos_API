using ProductosApi.DTOs;
using ProductosApi.Models;
using ProductosApi.Repositories;

namespace ProductosApi.Services;

public sealed class ProductoService(IProductoRepository productoRepository) : IProductoService
{
    public List<ProductoDto> ObtenerTodos()
    {
        return productoRepository.ObtenerTodos().Select(ToDto).ToList();
    }

    public ProductoDto? ObtenerPorId(int id)
    {
        var producto = productoRepository.ObtenerPorId(id);
        return producto is null ? null : ToDto(producto);
    }

    public List<ProductoDto> BuscarPorCategoria(string categoria)
    {
        return productoRepository.BuscarPorCategoria(categoria).Select(ToDto).ToList();
    }

    public ProductoDto Agregar(CreateProductoDto productoDto)
    {
        var producto = new Producto
        {
            Nombre = productoDto.Nombre,
            Precio = productoDto.Precio,
            Categoria = productoDto.Categoria,
            Activo = true
        };

        return ToDto(productoRepository.Agregar(producto));
    }

    public ProductoDto? Actualizar(int id, CreateProductoDto productoDto)
    {
        var productoExistente = productoRepository.ObtenerPorId(id);
        if (productoExistente is null)
        {
            return null;
        }

        var productoActualizado = new Producto
        {
            Id = id,
            Nombre = productoDto.Nombre,
            Precio = productoDto.Precio,
            Categoria = productoDto.Categoria,
            Activo = productoExistente.Activo
        };

        return productoRepository.Actualizar(id, productoActualizado)
            ? ToDto(productoActualizado)
            : null;
    }

    public bool Eliminar(int id)
    {
        return productoRepository.Eliminar(id);
    }

    private static ProductoDto ToDto(Producto producto)
    {
        return new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Precio = producto.Precio,
            Categoria = producto.Categoria,
            Activo = producto.Activo
        };
    }
}
