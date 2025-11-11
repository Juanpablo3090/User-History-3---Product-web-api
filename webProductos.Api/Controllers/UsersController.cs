
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webProductos.Infrastructure.Persistence;

namespace webProductos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _ctx;
    public UsersController(AppDbContext ctx) => _ctx = ctx;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll() => Ok(await _ctx.Users.Select(u => new { u.Id, u.UserName, u.Email, u.Role }).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var u = await _ctx.Users.FindAsync(id);
        if (u == null) return NotFound();
        return Ok(new { u.Id, u.UserName, u.Email, u.Role });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] dynamic body)
    {
        var u = await _ctx.Users.FindAsync(id);
        if (u == null) return NotFound();
        u.UserName = (string?)body.userName ?? u.UserName;
        u.Email = (string?)body.email ?? u.Email;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var u = await _ctx.Users.FindAsync(id);
        if (u == null) return NotFound();
        _ctx.Users.Remove(u);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
