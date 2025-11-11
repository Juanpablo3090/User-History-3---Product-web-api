using webProductos.Application.Interfaces;
using webProductos.Domain.Entities;
using webProductos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace webProductos.Infrastructure.Repositories;
public class ProductRepository : IProductService
{
    private readonly AppDbContext _ctx;
    public ProductRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<Product> CreateAsync(Product product)
    {
        _ctx.Products.Add(product);
        await _ctx.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var p = await _ctx.Products.FindAsync(id);
        if (p == null) return false;
        _ctx.Products.Remove(p);
        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _ctx.Products.ToListAsync();
    public async Task<Product?> GetByIdAsync(int id) => await _ctx.Products.FindAsync(id);
    public async Task<bool> UpdateAsync(Product product)
    {
        var exists = await _ctx.Products.FindAsync(product.Id);
        if (exists == null) return false;
        exists.Name = product.Name;
        exists.Description = product.Description;
        exists.Price = product.Price;
        await _ctx.SaveChangesAsync();
        return true;
    }
}
