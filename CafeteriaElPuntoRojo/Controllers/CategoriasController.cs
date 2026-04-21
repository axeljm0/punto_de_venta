using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeteriaElPuntoRojo.Data;
using CafeteriaElPuntoRojo.Models;

namespace CafeteriaElPuntoRojo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public CategoriasController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/categorias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
        var categorias = await _context.Categorias.ToListAsync();
        return Ok(categorias);
    }
    
    // POST: api/categorias
    [HttpPost]
    public async Task<ActionResult<Categoria>> CreateCategoria([FromBody] Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria.Nombre))
            return BadRequest(new { error = "El nombre de la categoría es requerido" });
            
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        
        return Ok(categoria);
    }
}