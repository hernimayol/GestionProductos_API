using ProductosApi.DTOs;
using ProductosApi.Models;
using ProductosApi.Repositories;

namespace ProductosApi.Services;

public sealed class ProductoService(IProductoRepository productoRepository) : IProductoService
{
    public IReadOnlyList<ProductoDto> GetAll()
    {
        return productoRepository.GetAll().Select(ToDto).ToList();
    }

    public ProductoDto? GetById(int id)
    {
        var producto = productoRepository.GetById(id);
        return producto is null ? null : ToDto(producto);
    }

    public ProductoDto Create(CreateProductoDto productoDto)
    {
        var producto = new Producto
        {
            Nombre = productoDto.Nombre,
            Precio = productoDto.Precio,
            Categoria = productoDto.Categoria,
            Activo = true
        };

        return ToDto(productoRepository.Add(producto));
    }

    public ProductoDto? Update(int id, CreateProductoDto productoDto)
    {
        var productoExistente = productoRepository.GetById(id);
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

        return productoRepository.Update(id, productoActualizado)
            ? ToDto(productoActualizado)
            : null;
    }

    public bool Delete(int id)
    {
        return productoRepository.Delete(id);
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
