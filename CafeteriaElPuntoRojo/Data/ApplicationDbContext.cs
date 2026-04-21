using Microsoft.EntityFrameworkCore;
using CafeteriaElPuntoRojo.Models;

namespace CafeteriaElPuntoRojo.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetallesVenta { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar precisión de decimales para MySQL
        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasPrecision(10, 2);
            
        modelBuilder.Entity<DetalleVenta>()
            .Property(d => d.PrecioUnitario)
            .HasPrecision(10, 2);
            
        modelBuilder.Entity<DetalleVenta>()
            .Property(d => d.Subtotal)
            .HasPrecision(10, 2);
            
        modelBuilder.Entity<Venta>()
            .Property(v => v.Total)
            .HasPrecision(10, 2);
        
        // Configurar autoincremento para MySQL
        modelBuilder.Entity<Categoria>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
            
        modelBuilder.Entity<Producto>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
            
        modelBuilder.Entity<Venta>()
            .Property(v => v.Id)
            .ValueGeneratedOnAdd();
            
        modelBuilder.Entity<DetalleVenta>()
            .Property(d => d.Id)
            .ValueGeneratedOnAdd();
        
        // Datos semilla (solo si no existen)
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nombre = "Cafés" },
            new Categoria { Id = 2, Nombre = "Pasteles" },
            new Categoria { Id = 3, Nombre = "Bebidas Frías" }
        );
        
        modelBuilder.Entity<Producto>().HasData(
            new Producto { Id = 1, Nombre = "Café Americano", Precio = 25, Stock = 50, CategoriaId = 1 },
            new Producto { Id = 2, Nombre = "Café Latte", Precio = 35, Stock = 40, CategoriaId = 1 },
            new Producto { Id = 3, Nombre = "Café Capuchino", Precio = 38, Stock = 35, CategoriaId = 1 },
            new Producto { Id = 4, Nombre = "Pastel de Chocolate", Precio = 45, Stock = 20, CategoriaId = 2 },
            new Producto { Id = 5, Nombre = "Pastel de Vainilla", Precio = 40, Stock = 15, CategoriaId = 2 },
            new Producto { Id = 6, Nombre = "Frappé de Vainilla", Precio = 55, Stock = 30, CategoriaId = 3 },
            new Producto { Id = 7, Nombre = "Frappé de Fresa", Precio = 55, Stock = 25, CategoriaId = 3 },
            new Producto { Id = 8, Nombre = "Brownie", Precio = 30, Stock = 18, CategoriaId = 2 }
        );
    }
}