using ProductosApi.Models;

namespace ProductosApi.Repositories;

public interface IProductoRepository
{
    Producto Agregar(Producto producto);
    List<Producto> ObtenerTodos();
    Producto? ObtenerPorId(int id);
    List<Producto> BuscarPorCategoria(string categoria);
    bool Actualizar(int id, Producto producto);
    bool Eliminar(int id);
}
