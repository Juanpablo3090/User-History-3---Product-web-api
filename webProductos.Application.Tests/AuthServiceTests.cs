
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using webProductos.Application.DTOs;
using webProductos.Application.Services;
using webProductos.Infrastructure.Persistence;
using webProductos.Domain.Entities;
using Xunit;

public class AuthServiceTests
{
    private AppDbContext CreateContext()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        var ctx = new AppDbContext(opts);
        return ctx;
    }

    [Fact]
    public async Task Register_CreatesUser()
    {
        var ctx = CreateContext();
        var inMemory = new Dictionary<string,string> { { "JwtSettings:Secret","test" }, {"JwtSettings:Issuer","i"}, {"JwtSettings:Audience","a"} };
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(inMemory).Build();
        var svc = new AuthService(ctx, cfg);

        var res = await svc.RegisterAsync(new RegisterDto { Username = "u1", Email = "e@e.com", Password = "pass" });
        Assert.True(res.Success);
        Assert.Equal(1, await ctx.Users.CountAsync());
    }

    [Fact]
    public async Task Login_WrongCredentials_Fails()
    {
        var ctx = CreateContext();
        var inMemory = new Dictionary<string,string> { { "JwtSettings:Secret","test" }, {"JwtSettings:Issuer","i"}, {"JwtSettings:Audience","a"} };
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(inMemory).Build();
        var svc = new AuthService(ctx, cfg);

        var res = await svc.LoginAsync(new LoginDto { Username = "no", Password = "x" });
        Assert.False(res.Success);
    }
}
