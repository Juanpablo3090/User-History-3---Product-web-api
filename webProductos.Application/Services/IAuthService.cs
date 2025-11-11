
using webProductos.Application.DTOs;

namespace webProductos.Application.Services;

public interface IAuthService
{
    Task<AuthResultDto> RegisterAsync(RegisterDto dto);
    Task<AuthResultDto> LoginAsync(LoginDto dto);
}
