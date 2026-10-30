namespace ProductosApi.DTOs;

public sealed class ProductoDto
{
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public decimal Precio { get; init; }
    public string Categoria { get; init; } = string.Empty;
    public bool Activo { get; init; }
}
