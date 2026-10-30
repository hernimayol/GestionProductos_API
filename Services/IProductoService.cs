using ProductosApi.DTOs;

namespace ProductosApi.Services;

public interface IProductoService
{
    IReadOnlyList<ProductoDto> GetAll();
    ProductoDto? GetById(int id);
    ProductoDto Create(CreateProductoDto productoDto);
    ProductoDto? Update(int id, CreateProductoDto productoDto);
    bool Delete(int id);
}
