using Application.Abstractions.IServices;
using Application.Dtos;

namespace Application.Services;

public class OrderService : IOrderService
{
    public Task<BaseResponse<OrderDto>> CreateOrderAsync(CreateOrderRequestModel model, int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllDeliveredOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllUnDeliveredOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<OrderProductDto>> GetOrderByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<OrderProductDto>> GetOrdersByCustomerIdAsync(string customerId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<OrderDto>> UpdateOrderAsync(string id, UpdateOrderRequestModel model)
    {
        throw new NotImplementedException();
    }
}
