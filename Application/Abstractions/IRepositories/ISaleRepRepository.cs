using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface ISaleRepRepository : IBaseRepository<SaleRep>
{
    Task<SaleRep> GetSaleRepAsync(string id);
    Task<bool> CheckIfEmailExistAsync(string email);
    Task<SaleRep> GetSaleRepAsync(Expression<Func<SaleRep, bool>> expression);
    Task<IEnumerable<SaleRep>> GetSelectedAsync(Expression<Func<SaleRep, bool>> expression);
    Task<IEnumerable<SaleRep>> GetAllAsync();
}
