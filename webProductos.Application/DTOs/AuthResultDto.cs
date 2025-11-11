
namespace webProductos.Application.DTOs;
public class AuthResultDto
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string[]? Errors { get; set; }
}
