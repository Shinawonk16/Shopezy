using Application.Abstractions.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository;

public class UserRepository : BaseRepository<User>,IUserRepository
{
    public UserRepository(ApplicationDbContext context):base(context)
    {
       

    }

    public async Task<IList<UserRole>> GetUserByRoleAsync(string role)
        {
            return await _context.UserRoles
            .Include(u => u.User)
            .Include(u => u.Role)
            .Where(u => u.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<UserRole> LoginAsync(string email, string password)
        {
            return await _context.UserRoles
            .Include(u => u.Role)
            .Include(u => u.User)
            .Where(u => u.User.Email.Equals(email,StringComparison.OrdinalIgnoreCase) && u.User.Password == password)
            .SingleOrDefaultAsync();
        }
}
