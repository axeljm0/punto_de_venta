using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeteriaElPuntoRojo.Data;

namespace CafeteriaElPuntoRojo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ReportesController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("ventas-diarias")]
    public async Task<IActionResult> GetVentasDiarias([FromQuery] DateTime fecha)
    {
        try
        {
            var ventas = await _context.Ventas
                .Where(v => v.Fecha.Date == fecha.Date)
                .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
                .ToListAsync();
                
            var totalVentas = ventas.Count;
            var totalIngresos = ventas.Sum(v => v.Total);
            
            var productosVendidos = ventas
                .SelectMany(v => v.Detalles)
                .GroupBy(d => new { d.ProductoId, d.Producto!.Nombre })
                .Select(g => new {
                    Producto = g.Key.Nombre,
                    Cantidad = g.Sum(d => d.Cantidad),
                    Total = g.Sum(d => d.Subtotal)
                })
                .ToList();
                
            return Ok(new {
                fecha = fecha.ToString("yyyy-MM-dd"),
                totalVentas = totalVentas,
                totalIngresos = totalIngresos,
                productosVendidos = productosVendidos
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet("inventario-bajo")]
    public async Task<IActionResult> GetInventarioBajo([FromQuery] int limite = 5)
    {
        try
        {
            var productos = await _context.Productos
                .Where(p => p.Stock <= limite)
                .Include(p => p.Categoria)
                .Select(p => new {
                    p.Id,
                    p.Nombre,
                    p.Stock,
                    CategoriaNombre = p.Categoria != null ? p.Categoria.Nombre : "Sin categoría"
                })
                .ToListAsync();
                
            return Ok(productos);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet("ventas-totales")]
    public async Task<IActionResult> GetVentasTotales()
    {
        try
        {
            var totalVentas = await _context.Ventas.CountAsync();
            var totalIngresos = await _context.Ventas.SumAsync(v => v.Total);
            var productoMasVendido = await _context.DetallesVenta
                .GroupBy(d => d.ProductoId)
                .Select(g => new {
                    ProductoId = g.Key,
                    Cantidad = g.Sum(d => d.Cantidad)
                })
                .OrderByDescending(x => x.Cantidad)
                .FirstOrDefaultAsync();
                
            return Ok(new {
                totalVentas = totalVentas,
                totalIngresos = totalIngresos,
                productoMasVendido = productoMasVendido
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}