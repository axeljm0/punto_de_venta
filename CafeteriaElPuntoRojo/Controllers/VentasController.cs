using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeteriaElPuntoRojo.Data;
using CafeteriaElPuntoRojo.Models;
using CafeteriaElPuntoRojo.Models.DTOs;

namespace CafeteriaElPuntoRojo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public VentasController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<ActionResult> RegistrarVenta([FromBody] VentaDto ventaDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                Total = ventaDto.Total,
                Detalles = new List<DetalleVenta>()
            };
            
            foreach (var item in ventaDto.Items)
            {
                var producto = await _context.Productos.FindAsync(item.ProductoId);
                if (producto == null)
                    return BadRequest($"Producto {item.ProductoId} no existe");
                    
                if (producto.Stock < item.Cantidad)
                    return BadRequest($"Stock insuficiente para {producto.Nombre}. Disponible: {producto.Stock}");
                    
                producto.Stock -= item.Cantidad;
                
                venta.Detalles.Add(new DetalleVenta
                {
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = producto.Precio,
                    Subtotal = producto.Precio * item.Cantidad
                });
            }
            
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return Ok(new { ventaId = venta.Id, mensaje = "Venta registrada exitosamente" });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
    {
        return await _context.Ventas
            .Include(v => v.Detalles)
            .ThenInclude(d => d.Producto)
            .OrderByDescending(v => v.Fecha)
            .ToListAsync();
    }
}