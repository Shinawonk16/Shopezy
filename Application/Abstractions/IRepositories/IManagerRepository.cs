using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface IManagerRepository : IBaseRepository<Managers>
{
    Task<UserRole> GetManagerAsync(string id);
    Task<UserRole> GetManagerByRoleAsync(string roleId);

    Task<bool> CheckIfEmailExistAsync(string email);
    Task<IEnumerable<UserRole>> GetAllManagerAsync();
    Task<Managers> GetAsync(Expression<Func<Managers, bool>> expression);
    Task<IEnumerable<Managers>> GetSelectedAsync(Expression<Func<Managers, bool>> expression);

}
