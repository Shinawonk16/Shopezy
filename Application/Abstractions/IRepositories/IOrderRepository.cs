using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order> GetOrderAsync(string id);
    // Task<IEnumerable<Order>> GetAllOrderAsync();
    Task<Order> GetAsync(Expression<Func<Order, bool>> expression);
    Task<IEnumerable<Order>> GetSelectedAsync(Expression<Func<Order, bool>> expression);
}
