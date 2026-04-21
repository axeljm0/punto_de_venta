using System.Text.Json.Serialization;

namespace CafeteriaElPuntoRojo.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public int CategoriaId { get; set; }
    
    [JsonIgnore]  // ⭐ Ignora esta propiedad al serializar
    public Categoria? Categoria { get; set; }
}