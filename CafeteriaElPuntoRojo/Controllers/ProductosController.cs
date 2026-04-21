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
            .OrderBy(p => p.Id)
            .ToListAsync();
        
        return Ok(productos);
    }
    
    // GET: api/productos/{id}
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
    
    // POST: api/productos (Crear nuevo producto)
    [HttpPost]
    public async Task<ActionResult<Producto>> CreateProducto([FromBody] Producto producto)
    {
        if (producto == null)
            return BadRequest(new { error = "Datos de producto inválidos" });
            
        if (string.IsNullOrWhiteSpace(producto.Nombre))
            return BadRequest(new { error = "El nombre del producto es requerido" });
            
        if (producto.Precio <= 0)
            return BadRequest(new { error = "El precio debe ser mayor a 0" });
            
        if (producto.Stock < 0)
            return BadRequest(new { error = "El stock no puede ser negativo" });
            
        try
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error al crear producto", detalle = ex.Message });
        }
    }
    
    // PUT: api/productos/{id} (Actualizar producto completo)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProducto(int id, [FromBody] Producto productoActualizado)
    {
        if (id != productoActualizado.Id)
            return BadRequest(new { error = "El ID no coincide" });
            
        var producto = await _context.Productos.FindAsync(id);
        
        if (producto == null)
            return NotFound(new { error = $"Producto con ID {id} no encontrado" });
        
        // Actualizar campos
        producto.Nombre = productoActualizado.Nombre;
        producto.Precio = productoActualizado.Precio;
        producto.Stock = productoActualizado.Stock;
        producto.CategoriaId = productoActualizado.CategoriaId;
        
        try
        {
            await _context.SaveChangesAsync();
            
            return Ok(new { 
                id = producto.Id,
                nombre = producto.Nombre,
                stockAnterior = producto.Stock,
                stockNuevo = productoActualizado.Stock,
                mensaje = $"Producto '{producto.Nombre}' actualizado correctamente"
            });
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new { error = "Error al guardar en la base de datos", detalle = ex.Message });
        }
    }
    
    // PATCH: api/productos/{id}/stock (Actualizar solo el stock)
    [HttpPatch("{id}/stock")]
    public async Task<IActionResult> UpdateStock(int id, [FromBody] int nuevoStock)
    {
        if (nuevoStock < 0)
            return BadRequest(new { error = "El stock no puede ser negativo" });
            
        var producto = await _context.Productos.FindAsync(id);
        
        if (producto == null)
            return NotFound(new { error = $"Producto con ID {id} no encontrado" });
        
        int stockAnterior = producto.Stock;
        producto.Stock = nuevoStock;
        
        await _context.SaveChangesAsync();
        
        return Ok(new { 
            id = producto.Id,
            nombre = producto.Nombre,
            stockAnterior = stockAnterior,
            stockNuevo = nuevoStock,
            mensaje = $"Stock de '{producto.Nombre}' actualizado de {stockAnterior} a {nuevoStock}"
        });
    }
    
    // DELETE: api/productos/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        
        if (producto == null)
            return NotFound(new { error = $"Producto con ID {id} no encontrado" });
        
        // Verificar si el producto tiene ventas asociadas
        var tieneVentas = await _context.DetallesVenta.AnyAsync(d => d.ProductoId == id);
        
        if (tieneVentas)
            return BadRequest(new { error = "No se puede eliminar el producto porque tiene ventas asociadas" });
        
        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        
        return Ok(new { mensaje = $"Producto '{producto.Nombre}' eliminado correctamente" });
    }
}