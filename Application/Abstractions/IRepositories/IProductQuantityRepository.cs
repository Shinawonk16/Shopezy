using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface IProductQuantityRepository:IBaseRepository<ProductQuantity>
{

    public Task<ProductQuantity> GetProductQuantityById(string productQuantityId);
    public Task<IList<ProductQuantity>> GetProductQuantities();
    public Task AddRestockEvent(string productQuantityId, RestockEvent restockEvent);
}
