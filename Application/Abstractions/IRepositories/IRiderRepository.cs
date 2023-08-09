using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface IRiderRepository : IBaseRepository<Rider>
{
    Task<Rider> GetRiderAsync(string id);
    Task<IEnumerable<Rider>> GetAllRiderAsync();
    Task<bool> CheckIfEmailExistAsync(string email);
    Task<Rider> GetRiderAsync(Expression<Func<Rider, bool>> expression);
    Task<IEnumerable<Rider>> GetAllRiderAsync(Expression<Func<Rider, bool>> expression);
}
