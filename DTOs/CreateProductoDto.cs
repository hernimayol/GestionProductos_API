using System.ComponentModel.DataAnnotations;

namespace ProductosApi.DTOs;

public sealed class CreateProductoDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ParseLimitsInInvariantCulture = true)]
    public decimal Precio { get; set; }

    [Required]
    public string Categoria { get; set; } = string.Empty;
}
