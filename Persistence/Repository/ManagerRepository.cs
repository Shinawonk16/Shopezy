using System.Linq.Expressions;
using Application.Abstractions.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository;

public class ManagerRepository : BaseRepository<Managers>, IManagerRepository
{
    public ManagerRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<bool> CheckIfEmailExistAsync(string email)
    {
        return await _context.Managers
        .Include(u => u.User)
        .Where(e => e.User.Email.Equals(email,StringComparison.OrdinalIgnoreCase)).AnyAsync();
    }

    public async Task<IEnumerable<Managers>> GetAllManagerAsync()
    {
        return await _context.Managers
        .Include(u => u.User)
        .Include(u => u.IsDeleted == false)
        .ToListAsync();
    }

    public async Task<Managers> GetAsync(Expression<Func<Managers, bool>> expression)
    { 
        return await _context.Managers
        .Include(u => u.User)
        .Where(c => c.IsDeleted == false)
        .SingleOrDefaultAsync(expression);
    }

    public async Task<Managers> GetManagerAsync(string id)
    {
        return await _context.Managers
        .Include(u => u.User)
        .Include(u => u.Id == id && u.IsDeleted == false)
        .SingleOrDefaultAsync();
    }

    public async Task<UserRole> GetManagerByRoleAsync(string roleId)
    {
        return await _context.UserRoles
        .Include(r => r.Role)
        .Include(r => r.User)
        .Include(r => r.Role.Id == roleId)
        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Managers>> GetSelectedAsync(Expression<Func<Managers, bool>> expression)
    {
        return await _context.Managers
        .Include(u => u.User)
        .Where(c => c.IsDeleted == false)
        .ToListAsync();
    }
}
