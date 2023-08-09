using Application.Abstractions.IRepositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repository;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<IEnumerable<Review>> GetAllReviewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Review>> GetReviewByProductIdAsync(string productId)
    {
        throw new NotImplementedException();
    }
}
