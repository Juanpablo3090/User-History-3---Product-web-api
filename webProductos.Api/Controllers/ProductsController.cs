
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webProductos.Domain.Entities;
using webProductos.Infrastructure.Persistence;

namespace webProductos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _ctx;
    public ProductsController(AppDbContext ctx) => _ctx = ctx;

    [HttpPost]
    public async Task<IActionResult> Create(Product p)
    {
        _ctx.Products.Add(p);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = p.Id }, p);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _ctx.Products.ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _ctx.Products.FindAsync(id);
        if (p == null) return NotFound();
        return Ok(p);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Product updated)
    {
        var p = await _ctx.Products.FindAsync(id);
        if (p == null) return NotFound();
        p.Name = updated.Name;
        p.Description = updated.Description;
        p.Price = updated.Price;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await _ctx.Products.FindAsync(id);
        if (p == null) return NotFound();
        _ctx.Products.Remove(p);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
