using System.Linq.Expressions;
using Application.Abstractions.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Review>> GetAllReviewAsync()
    {
        return await _context.Reviews
         .Include(c => c.Customer)
         .ThenInclude(x => x.User)
         .Where(x => x.IsDeleted == false )
         .OrderByDescending(x => x.CreatedAt)
         .ToListAsync();
    }

    public async Task<Review> GetReviewAsync(Expression<Func<Review, bool>> expression)
    {
        return await _context.Reviews
         .Include(c => c.Customer)
         .ThenInclude(x => x.User)
         .Where(x => x.IsDeleted == false)
         .OrderByDescending(x => x.CreatedAt)
         .SingleOrDefaultAsync(expression);

    }

    public async Task<IEnumerable<Review>> GetReviewByProductIdAsync(string productId)
    {
         return await _context.Reviews
         .Include(c => c.Customer)
         .ThenInclude(x => x.User)
         .Where(x => x.IsDeleted == false && x.Product.Id == productId)
         .OrderByDescending(x => x.CreatedAt)
         .ToListAsync();
    }
}
