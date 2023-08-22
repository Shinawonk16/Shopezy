using System.Linq.Expressions;
using Application.Abstractions.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository
{
    public class CartRepository : BaseRepository<Carts>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<Carts>> GetAllCartsAsync()
        {
             return await _context.Carts
        .Include(a => a.Customer)
        .Where(a => a.IsDeleted == false)
        .ToListAsync();
        }

        public async Task<Carts> GetCartsByCustomerEmailAsync(string email)
        {
             return await _context.Carts
        .Include(a => a.Customer)
        .Where(a => a.IsDeleted == false)
        .SingleOrDefaultAsync(x => x.Customer.User.Email == email);
        }

        public async Task<Carts> GetCartsAsync(Expression<Func<Carts, bool>> expression)
        {
            return await _context.Carts
        .Include(a => a.Customer)
        .Where(a => a.IsDeleted == false)
        .SingleOrDefaultAsync(expression);
        }
    }
}