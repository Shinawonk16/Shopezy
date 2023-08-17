using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<IEnumerable<Review>> GetReviewByProductIdAsync(string productId);
    Task<Review> GetReviewAsync(Expression<Func<Review, bool>> expression);

    Task<IEnumerable<Review>> GetAllReviewAsync();
}
