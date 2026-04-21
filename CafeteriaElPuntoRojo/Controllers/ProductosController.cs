using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeteriaElPuntoRojo.Data;
using CafeteriaElPuntoRojo.Models;

namespace CafeteriaElPuntoRojo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ProductosController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        var productos = await _context.Productos
            .Include(p => p.Categoria)
            .ToListAsync();
        
        return Ok(productos);
    }
    
    // GET: api/productos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);
            
        if (producto == null)
            return NotFound(new { mensaje = $"Producto con ID {id} no encontrado" });
            
        return Ok(producto);
    }
    
    // PUT: api/productos/5 (Actualizar stock)
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarStock(int id, [FromBody] int nuevoStock)
    {
        if (nuevoStock < 0)
            return BadRequest(new { error = "El stock no puede ser negativo" });
            
        var producto = await _context.Productos.FindAsync(id);
        
        if (producto == null)
            return NotFound(new { error = $"Producto con ID {id} no encontrado" });
        
        int stockAnterior = producto.Stock;
        producto.Stock = nuevoStock;
        
        try
        {
            await _context.SaveChangesAsync();
            
            return Ok(new { 
                id = producto.Id,
                nombre = producto.Nombre,
                stockAnterior = stockAnterior,
                stockNuevo = nuevoStock,
                mensaje = $"Stock de '{producto.Nombre}' actualizado de {stockAnterior} a {nuevoStock}"
            });
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new { error = "Error al guardar en la base de datos", detalle = ex.Message });
        }
    }
    
    // POST: api/productos (Crear nuevo producto - opcional)
    [HttpPost]
    public async Task<ActionResult<Producto>> CreateProducto([FromBody] Producto producto)
    {
        if (producto == null)
            return BadRequest(new { error = "Datos de producto inválidos" });
            
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
    }
}