using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface IManagerRepository : IBaseRepository<Managers>
{
    Task<Managers> GetManagerAsync(string id);
    Task<UserRole> GetManagerByRoleAsync(string roleId);

    Task<bool> CheckIfEmailExistAsync(string email);
    Task<IEnumerable<Managers>> GetAllManagerAsync();
    Task<Managers> GetAsync(Expression<Func<Managers, bool>> expression);
    Task<IEnumerable<Managers>> GetSelectedAsync(Expression<Func<Managers, bool>> expression);

}
