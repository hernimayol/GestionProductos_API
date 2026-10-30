using ProductosApi.Models;

namespace ProductosApi.Repositories;

public sealed class ProductoRepositoryInMemory : IProductoRepository
{
    private static readonly object SyncRoot = new();
    private static readonly List<Producto> Productos = [];
    private static int _nextId = 1;

    public IReadOnlyList<Producto> GetAll()
    {
        lock (SyncRoot)
        {
            return Productos.Select(Copiar).ToList();
        }
    }

    public Producto? GetById(int id)
    {
        lock (SyncRoot)
        {
            var producto = Productos.FirstOrDefault(producto => producto.Id == id);
            return producto is null ? null : Copiar(producto);
        }
    }

    public Producto Add(Producto producto)
    {
        lock (SyncRoot)
        {
            var nuevoProducto = Copiar(producto);
            nuevoProducto.Id = _nextId++;
            Productos.Add(nuevoProducto);
            return Copiar(nuevoProducto);
        }
    }

    public bool Update(int id, Producto producto)
    {
        lock (SyncRoot)
        {
            var indice = Productos.FindIndex(productoExistente => productoExistente.Id == id);
            if (indice < 0)
            {
                return false;
            }

            var productoActualizado = Copiar(producto);
            productoActualizado.Id = id;
            Productos[indice] = productoActualizado;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (SyncRoot)
        {
            var indice = Productos.FindIndex(producto => producto.Id == id);
            if (indice < 0)
            {
                return false;
            }

            Productos.RemoveAt(indice);
            return true;
        }
    }

    private static Producto Copiar(Producto producto)
    {
        return new Producto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Precio = producto.Precio,
            Categoria = producto.Categoria,
            Activo = producto.Activo
        };
    }
}
