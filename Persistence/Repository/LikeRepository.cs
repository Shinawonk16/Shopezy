using Application.Abstractions.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository;

public class LikeRepository : BaseRepository<Like>, ILikeRepository
{
    public LikeRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<IEnumerable<Like>> GetLikesByReviewIdAsync(string reviwId)
    {
        return await _context.Likes
        .Include(u => u.User)
        .Where(c => c.IsDeleted == false && c.Review.Id == reviwId)
        .ToListAsync();
    }
}
