using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions.IServices;

public interface ICustomerService
{
    public Task<BaseResponse<CustomerDto>> CreateCustomerAsync(CreateCustomerRequestModel model);
    public Task<BaseResponse<CustomerDto>> GetByIdAsync(string id);
    public Task<BaseResponse<CustomerDto>> GetByEmailAsync(string email);
    public Task<BaseResponse<CustomerDto>> UpdateCustomerAsync(string id, UpdateCustomerRequestModel model);
    public Task<BaseResponse<IEnumerable<CustomerDto>>> GetAllAsync();
    public Task<BaseResponse<IEnumerable<CustomerDto>>> GetSelectedAsync();

}
