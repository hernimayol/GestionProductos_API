using ProductosApi.DTOs;

namespace ProductosApi.Services;

public interface IProductoService
{
    List<ProductoDto> ObtenerTodos();
    ProductoDto? ObtenerPorId(int id);
    List<ProductoDto> BuscarPorCategoria(string categoria);
    ProductoDto Agregar(CreateProductoDto productoDto);
    ProductoDto? Actualizar(int id, CreateProductoDto productoDto);
    bool Eliminar(int id);
}
