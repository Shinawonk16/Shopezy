using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface ILikeRepository:IBaseRepository<Like>
{
        Task<IEnumerable<Like>> GetLikesByReviewIdAsync(string reviwId);

}
