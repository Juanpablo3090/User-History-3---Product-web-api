using Microsoft.EntityFrameworkCore;
using webProductos.Domain.Entities;

namespace webProductos.Infrastructure.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
}
