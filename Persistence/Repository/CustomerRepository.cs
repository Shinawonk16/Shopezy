using System.Linq.Expressions;
using Application.Abstractions.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {

    }
    public async Task<bool> CheckIfEmailExistAsync(string email)
    {
        return await _context.Customers
        .Include(c => c.User)
        .Where(x => x.User.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).AnyAsync();

    }

    public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
    {
        return await _context.Customers
            .Include(c => c.User)
            .Include(c => c.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<Customer> GetAsync(string id)
    {
        return await _context.Customers
        .Include(c => c.User)
        .Where(c => c.IsDeleted == false)
        .SingleOrDefaultAsync(c => c.User.Id == id);

    }

    public async Task<Customer> GetAsync(Expression<Func<Customer, bool>> expression)
    {
        return await _context.Customers
        .Include(c => c.User)
        .Where(c => c.IsDeleted == false)
        .SingleOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<Customer>> GetSelectedAsync(Expression<Func<Customer, bool>> expression)
    {
        return await _context.Customers
        .Include(c => c.User)
        .Where(c => c.IsDeleted == false)
        .ToListAsync();
    }
}
