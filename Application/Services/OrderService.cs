// using Application.Abstractions.IRepositories;
// using Application.Abstractions.IServices;
// using Application.Dtos;
// using Domain.Entities;

// namespace Application.Services;

// public class OrderService : IOrderService
// {
//     private readonly IOrderRepository _orderRepository;
//     private readonly IProductRepository _productRepository;
//     private readonly ICustomerRepository _customerRepository;
//     private readonly IOrderProductRepository _orderProductRepository;
//     private readonly ISaleRepository _saleRepository;
//     private readonly ICartRepository _cartRepository;


//     public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository, IOrderProductRepository orderProductRepository = null, ISaleRepository saleRepository = null, ICartRepository cartRepository = null)
//     {
//         _orderRepository = orderRepository;
//         _productRepository = productRepository;
//         _customerRepository = customerRepository;
//         _orderProductRepository = orderProductRepository;
//         _saleRepository = saleRepository;
//         _cartRepository = cartRepository;
//     }

//     public async Task<BaseResponse<CartDto>> AddItemToCart(string customerId, CreateCartRequestModel model)
//     {
//         var getCustomer = await _customerRepository.GetAsync(customerId);
//         if (getCustomer != null)
//         {
//             var availableItems = new List<CartItemModel>();
//             var distinctCartItems = model.CartItems.GroupBy(x => x.ProductId).Selec(a => new CartItemModel { ProductId = a.Key })

//         }

//     }

//     public Task<BaseResponse<bool>> CheckOut(string CustomerName, OrderRequestModel model)
//     {
//         throw new NotImplementedException();
//     }

//     public async Task<BaseResponse<OrderDto>> CreateOrderAsync(CreateOrderRequestModel model, string customerId)
//     {
//         var customer = await _customerRepository.GetAsync(x => x.Id == customerId);
//         if (customer != null)
//         {

//             foreach (var id in model.OrderRequestModels)
//             {
//                 var product = await _productRepository.GetAsync(id.ProductId);
//                 if (product != null)
//                 {
//                     var quantity = id.Quantity;

//                 }
//             }
//             var order = new Order
//             {

//             };
//         }
//         var existingCart = _cartRepository.GetCartsAsync(x => x.Customer.Id == customerId);
//         var response = new List<string>();
//         var availableItems = new List<CartItemModel>();

//         // Ensuring distinct items in the CartItems list
//         var distinctCartItems = model.OrderRequestModels
//             .GroupBy(item => item.ProductId)
//             .Select(x => new CartItemModel
//             {
//                 ProductId = x.Key,
//                 Quantity = x.Sum(item => item.Quantity)
//             })
//             .ToList();

//         foreach (var item in distinctCartItems)
//         {
//             var product = _productRepository.GetAsync(x => x.Id == item.ProductId);

//             if (product is null)
//             {
//                 response.Add($"Invalid ProductId: {item.ProductId}");
//                 continue;
//             }

//             if (!product.IsAvailable)
//             {
//                 response.Add($"Product not available: {product.Name}");
//                 continue;
//             }

//             var availableQuantity = Math.Min(product.Quantity, item.Quantity);
//             availableItems.Add(new CartItemModel
//             {
//                 ProductId = product.Id,
//                 Quantity = availableQuantity
//             });

//             product.Quantity -= availableQuantity;
//             _repository.Update(product);
//         }

//         var cartItems = availableItems.Select(c => new CartItem
//         {
//             ProductId = c.ProductId,
//             Unit = c.Quantity,
//             CostPrice = GetProductPrice(c.ProductId)
//         }).ToList();

//         if (existingCart is not null)
//         {
//             UpdateExistingCart(existingCart, cartItems);
//             return CreateCartDto(existingCart, response);
//         }

//         // var newCart = CreateNewCart(customerName, cartItems);
//         // return CreateCartDto(newCart, response);



//         return new BaseResponse<OrderDto> { };
//     }

//     public Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllDeliveredOrdersAsync()
//     {
//         throw new NotImplementedException();
//     }

//     public Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllOrdersAsync()
//     {
//         throw new NotImplementedException();
//     }

//     public Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllUnDeliveredOrdersAsync()
//     {
//         throw new NotImplementedException();
//     }

//     public Task<BaseResponse<OrderProductDto>> GetCart(string email)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<BaseResponse<OrderProductDto>> GetOrderByIdAsync(string id)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<BaseResponse<OrderProductDto>> GetOrdersByCustomerIdAsync(string customerId)
//     {
//         throw new NotImplementedException();
//     }

//     public Task<BaseResponse<OrderDto>> UpdateOrderAsync(string id, UpdateOrderRequestModel model)
//     {
//         throw new NotImplementedException();
//     }
// }
