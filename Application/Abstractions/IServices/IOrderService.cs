using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface IOrderService
{
    Task<BaseResponse<OrderDto>> CreateOrderAsync(CreateOrderRequestModel model, int customerId);
    Task<BaseResponse<OrderDto>> UpdateOrderAsync(string id, UpdateOrderRequestModel model);
    Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllDeliveredOrdersAsync();
    Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllOrdersAsync();
    Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllUnDeliveredOrdersAsync();
    Task<BaseResponse<OrderProductDto>> GetOrderByIdAsync(string id);
    Task<BaseResponse<OrderProductDto>> GetOrdersByCustomerIdAsync(string customerId);

}
