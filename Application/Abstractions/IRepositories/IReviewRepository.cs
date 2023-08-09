using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<IEnumerable<Review>> GetReviewByProductIdAsync(string productId);
    Task<IEnumerable<Review>> GetAllReviewAsync();
}
