using ProductosApi.Models;

namespace ProductosApi.Repositories;

public interface IProductoRepository
{
    IReadOnlyList<Producto> GetAll();
    Producto? GetById(int id);
    Producto Add(Producto producto);
    bool Update(int id, Producto producto);
    bool Delete(int id);
}
