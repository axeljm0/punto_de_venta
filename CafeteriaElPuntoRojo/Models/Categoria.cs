using System.Text.Json.Serialization;

namespace CafeteriaElPuntoRojo.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    
    // ⭐ Quita o comenta esta línea para romper el ciclo
    // [JsonIgnore]  // Opcional: ignora esta propiedad al serializar
    // public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}