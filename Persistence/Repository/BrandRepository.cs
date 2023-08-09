using System.Linq.Expressions;
using Application.Abstractions.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository;

public class BrandRepository : BaseRepository<Brand>, IBrandRepository
{
    public BrandRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<Brand> GetBrand(Expression<Func<Brand, bool>> expression)
    {
        return await _context.Brands
        .Include(a => a.Products)
        .Where(a => a.IsDeleted == false)
        .SingleOrDefaultAsync(expression);
    }

    public async Task<Brand> GetBrandAsync(string id)
    {
       return await _context.Brands
        .Include(a => a.Products)
        .Where(a => a.IsDeleted == false)
        .SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Product> GetBrandByProductIdAsync(string productId)
    {
        return await _context.Products
        .Include(a => a.Brand)
        .Where(a => a.IsDeleted == false && a.Id == productId)
        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Brand>> GetBrandsAsync()
    {
        return await _context.Brands
        .Include(a => a.Products)
        .Where(a => a.IsDeleted == false)
        .ToListAsync();
    }
}
