using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface ICartRepository : IBaseRepository<Carts>
{
    Task<Carts> GetCartsByCustomerEmailAsync(string email);
    Task<Carts> GetCartsAsync(Expression<Func<Carts, bool>> expression);

    Task<IList<Carts>> GetAllCartsAsync();


}
