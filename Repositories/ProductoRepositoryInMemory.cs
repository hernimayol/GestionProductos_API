using ProductosApi.Models;

namespace ProductosApi.Repositories;

public sealed class ProductoRepositoryInMemory : IProductoRepository
{
    private static readonly object SyncRoot = new();
    private static readonly List<Producto> Productos =
    [
        new Producto
        {
            Id = 1,
            Nombre = "Yerba mate",
            Precio = 3200m,
            Categoria = "Almacén",
            Activo = true
        },
        new Producto
        {
            Id = 2,
            Nombre = "Leche entera",
            Precio = 1400m,
            Categoria = "Lácteos",
            Activo = true
        },
        new Producto
        {
            Id = 3,
            Nombre = "Jugo de naranja",
            Precio = 2100m,
            Categoria = "Bebidas",
            Activo = true
        },
        new Producto
        {
            Id = 4,
            Nombre = "Queso cremoso",
            Precio = 5800m,
            Categoria = "Lácteos",
            Activo = false
        }
    ];
    private static int _nextId = 5;

    public Producto Agregar(Producto producto)
    {
        lock (SyncRoot)
        {
            var nuevoProducto = Copiar(producto);
            nuevoProducto.Id = _nextId++;
            Productos.Add(nuevoProducto);
            return Copiar(nuevoProducto);
        }
    }

    public List<Producto> ObtenerTodos()
    {
        lock (SyncRoot)
        {
            return Productos.Select(Copiar).ToList();
        }
    }

    public Producto? ObtenerPorId(int id)
    {
        lock (SyncRoot)
        {
            var producto = Productos.FirstOrDefault(producto => producto.Id == id);
            return producto is null ? null : Copiar(producto);
        }
    }

    public List<Producto> BuscarPorCategoria(string categoria)
    {
        lock (SyncRoot)
        {
            return Productos
                .Where(producto => string.Equals(
                    producto.Categoria,
                    categoria,
                    StringComparison.OrdinalIgnoreCase))
                .Select(Copiar)
                .ToList();
        }
    }

    public bool Actualizar(int id, Producto producto)
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

    public bool Eliminar(int id)
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
