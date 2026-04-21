namespace CafeteriaElPuntoRojo.Models.DTOs;

public class VentaDto
{
    public decimal Total { get; set; }
    public List<DetalleVentaDto> Items { get; set; } = new();
}

public class DetalleVentaDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}