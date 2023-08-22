using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions.IServices;

public interface IProductQuantityService
{
     Task<BaseResponse<ProductQuantityDto>> GetProductQuantityByIdAsync(string productQuantityId);
     Task<BaseResponse<IList<ProductQuantityDto>>> GetProductQuantities();
    Task AddRestockEventAsync(string productQuantityId, RestockEvent restockEvent);
    Task UpdateRestockEventAsync(string productQuantityId, RestockEvent restockEvent);

    
}
