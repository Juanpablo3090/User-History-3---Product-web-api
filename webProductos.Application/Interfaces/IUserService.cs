using webProductos.Domain.Entities;
using System.Threading.Tasks;

namespace webProductos.Application.Interfaces;
public interface IUserService
{
    Task<User?> GetByIdAsync(int id);
}
