using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface IOrderService
{
    Task<BaseResponse<OrderDto>> CreateOrderAsync(CreateOrderRequestModel model, string customerId);
    // Task<BaseResponse<CartDto>> AddItemToCart(string customerId, CreateCartRequestModel model);
    Task<BaseResponse<OrderDto>> UpdateOrderAsync(string id, UpdateOrderRequestModel model);
    // Task<BaseResponse<bool>> CheckOut(string CustomerName, OrderRequestModel model);
    Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllDeliveredOrdersAsync();
    Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllOrdersAsync();
    Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllUnDeliveredOrdersAsync();
    Task<BaseResponse<IList<OrderProductDto>>> GetOrderByIdAsync(string id);
    Task<BaseResponse<OrderProductDto>> GetCart(string email);
    Task<BaseResponse<IList<OrderProductDto>>> GetOrdersByCustomerIdAsync(string customerId);

}
