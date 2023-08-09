using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Abstractions.IRepositories;


public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category> GetCategoryAsync(string id);
    Task<Category> GetCategoryAsync(Expression<Func<Category, bool>> expression);
    Task<IEnumerable<Category>> GetSelectedAsync(Expression<Func<Category, bool>> expression);
    Task<IEnumerable<Category>> GetAllCategoryAsync();
}
