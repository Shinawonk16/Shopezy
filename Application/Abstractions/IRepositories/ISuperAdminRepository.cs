using System.Linq.Expressions;
using Domain.Entities;
namespace Application.Abstractions.IRepositories;

public interface ISuperAdminRepository : IBaseRepository<SuperAdmin>
{
    Task<SuperAdmin> GetSuperAdminAsync(string id);
    Task<bool> CheckIfEmailExistAsync(string email);
    Task<IEnumerable<SuperAdmin>> GetAllSuperAdminAsync();
    Task<SuperAdmin> GetAsync(Expression<Func<SuperAdmin, bool>> expression);
    Task<IEnumerable<SuperAdmin>> GetSelectedAsync(Expression<Func<SuperAdmin, bool>> expression);

}
