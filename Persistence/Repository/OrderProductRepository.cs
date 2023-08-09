using System.Linq.Expressions;
using Application.Abstractions.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository;

public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderProduct>> GetAllDeleiveredOrderByProductIdForTheYearAsync(string productId, int year)
    {
        return await _context.OrderProducts
        .Include(x => x.Order)
        .Include(x => x.Order.Address)
        .Include(x => x.Order.Customer)
        .Include(x => x.Order.Customer.User)
        .Include(x => x.Product)
        .Where(x => x.Product.Id == productId && x.Order.IsDelivered == true && x.Order.UpdatedAt.Year == year)
        .ToListAsync();
    }

    public async Task<IEnumerable<OrderProduct>> GetAllDeliveredOrderByProductIdForTheMonthAsync(string productId, int month, int year)
    {
        return await _context.OrderProducts
        .Include(x => x.Order)
        .Include(x => x.Order.Address)
        .Include(x => x.Order.Customer)
        .Include(x => x.Order.Customer.User)
        .Include(x => x.Product)
        .Where(x => x.Product.Id == productId && x.Order.IsDelivered == true && x.Order.UpdatedAt.Month == month && x.Order.UpdatedAt.Year == year)
        .ToListAsync();
    }

    public async Task<IEnumerable<OrderProduct>> GetAllOrderAsync()
    {
        return await _context.OrderProducts
            .Include(x => x.Product)
            .Include(x => x.Order.Address)
            .Include(x => x.Order)
            .ThenInclude(x => x.Customer)
            .ThenInclude(c => c.User)
            .Where(x => x.Order.IsDelivered == true)
            .ToListAsync();
    }

    public async Task<OrderProduct> GetAsync(Expression<Func<OrderProduct, bool>> expression)
    {
        return await _context.OrderProducts
            .Include(x => x.Product)
            .Include(x => x.Order.Address)
            .Include(x => x.Order)
            .ThenInclude(x => x.Customer)
            .ThenInclude(c => c.User)
            .Where(x => x.Order.IsDelivered == true)
            .SingleOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<OrderProduct>> GetSelectedAsync(Expression<Func<OrderProduct, bool>> expression)
    {
        return await _context.OrderProducts
            .Include(x => x.Product)
            .Include(x => x.Order.Address)
            .Include(x => x.Order)
            .ThenInclude(x => x.Customer)
            .ThenInclude(c => c.User)
            .Where(x => x.Order.IsDelivered == true)
            .ToListAsync();
    }
}
