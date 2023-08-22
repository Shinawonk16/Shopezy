using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface IUserRepository:IBaseRepository<User>
{
    Task<UserRole> LoginAsync(string email,string password);
    Task<IList<UserRole>> GetUserByRoleAsync(string role);
}
