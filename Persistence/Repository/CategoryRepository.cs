using System.Linq.Expressions;
using Application.Abstractions.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<IEnumerable<Category>> GetAllCategoryAsync()
    {
        return await _context.Categories
        .Include(a => a.Product)
        .Where(a => a.IsDeleted == false).ToListAsync();
    }

    public async Task<Category> GetCategoryAsync(string id)
    {
        return await _context.Categories
        .Include(a => a.Product)
        .Where(a => a.IsDeleted == false)
        .SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Category> GetCategoryAsync(Expression<Func<Category, bool>> expression)
    {
        return await _context.Categories
        .Include(a => a.Product)
        .Where(a => a.IsDeleted == false)
        .SingleOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<Category>> GetSelectedAsync(Expression<Func<Category, bool>> expression)
    {
        return await _context.Categories
        .Include(a => a.Product)
        .Where(a => a.IsDeleted == false)
        .ToListAsync();
    }
}
