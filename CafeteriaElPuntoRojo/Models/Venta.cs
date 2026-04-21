namespace CafeteriaElPuntoRojo.Models;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public decimal Total { get; set; }
    
    // Relación: una venta tiene muchos detalles
    public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
}