using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services
{
    public class OrderServices : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductQuantityRepository _productquantityRepository;

        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderProductRepository _orderProductRepository;

        private readonly ISaleRepository _saleRepository;
        private readonly ICartRepository _cartRepository;


        public OrderServices(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository, IOrderProductRepository orderProductRepository = null, ISaleRepository saleRepository = null, ICartRepository cartRepository = null)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _orderProductRepository = orderProductRepository;
            _saleRepository = saleRepository;
            _cartRepository = cartRepository;
        }

        // public Task<BaseResponse<CartDto>> AddItemToCart(string customerId, CreateCartRequestModel model)
        // {
        //     throw new NotImplementedException();
        // }

        // public Task<BaseResponse<bool>> CheckOut(string CustomerName, OrderRequestModel model)
        // {
        //     throw new NotImplementedException();
        // }

        public async Task<BaseResponse<OrderDto>> CreateOrderAsync(CreateOrderRequestModel model, string customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId);
            foreach (var productId in model.OrderRequestModels)
            {
                var product = await _productRepository.GetAsync(productId.ProductId);
                if (product == null)
                {
                    return new BaseResponse<OrderDto>
                    {
                        Message = "Product not found",
                        Status = false
                    };
                }
            }
            if (customer == null)
            {
                return new BaseResponse<OrderDto>
                {
                    Message = "Customer not found",
                    Status = false
                };
            }

            var productions = await _productquantityRepository.GetProductQuantities();
            foreach (var order in model.OrderRequestModels)
            {
                bool isRemaining = false;
                decimal quantityOrdered = order.Quantity;
                foreach (var item in productions)
                {
                    var production = await _productquantityRepository.GetProductQuantityById(order.ProductId);
                    if (production != null && production.RestockEvents.QuantityAdded > 0 && quantityOrdered > production.RestockEvents.QuantityAdded)
                    {
                        quantityOrdered -= production.RestockEvents.QuantityAdded;
                        production.RestockEvents.QuantityAdded = 0;
                    }
                    else if (production != null && production.RestockEvents.QuantityAdded >= quantityOrdered)
                    {
                        production.RestockEvents.QuantityAdded -= (int)quantityOrdered;
                        quantityOrdered = 0;
                    }
                    if (quantityOrdered == 0)
                    {
                        isRemaining = true;
                        // await _productionRepository.UpdateAsync(production);
                        break;
                    }
                    else
                    {
                        isRemaining = false;
                    }
                    continue;
                }
                if (isRemaining == false)
                {
                    return new BaseResponse<OrderDto>
                    {
                        Message = $"Quantiy remaining is not up to the quantity you request for",
                        Status = false
                    };
                }

            }

            var ord = new Order
            {
                CustomerId = customer.Id,
                IsDelivered = false,
                Address = new Address
                {
                    PostalCode = model.Address.PostalCode,
                    City = model.Address.City,
                    State = model.Address.State,
                    Street = model.Address.Street,
                }
            };
            var cord = await _orderRepository.CreateAsync(ord);
            foreach (var item in model.OrderRequestModels)
            {
                var orderProduct = new OrderProduct
                {
                    ProductId = item.ProductId,
                    OrderId = cord.Id,
                    Quantity = item.Quantity,
                };
                await _orderProductRepository.CreateAsync(orderProduct);
                await _orderProductRepository.SaveAsync();

            }
            // await _productionRepository.SaveChangesAsync();
            return new BaseResponse<OrderDto>
            {
                Message = cord.Id.ToString(),
                Status = true
            };
        }

        public async Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllDeliveredOrdersAsync()
        {
            var getAll = await _orderRepository.GetSelectedAsync(x => x.IsDelivered == true);
            if (getAll.Count() != 0)
            {
                List<OrderProductDto> allOrders = new List<OrderProductDto>();
                foreach (var order in getAll)
                {
                    var productOrders = await _orderProductRepository.GetAsync(x => x.Order.Id == order.Id);
                    var orderProduct = new OrderProductDto
                    {


                        AddressDto = new AddressDto
                        {
                            State = productOrders[0].Order.Address.State,
                            City = productOrders[0].Order.Address.City,
                            Street = productOrders[0].Order.Address.Street,
                            PostalCode = productOrders[0].Order.Address.PostalCode,
                        },
                        OrderDto = productOrders.Select(x => new OrderDto
                        {
                            Product = new ProductDto
                            {
                                ProductName = x.Product.ProductName,
                                Price = x.Product.Price,
                                Image = x.Product.Image,
                                IsAvailable = x.Product.IsAvailable,
                                Description = x.Product.Description
                            },
                            Customer = new CustomerDto
                            {
                                Users = new UserDto
                                {
                                    UserName = $"{productOrders[0].Order.Customer.User.FirstName} {productOrders[0].Order.Customer.User.LastName}",
                                    PhoneNumber = productOrders[0].Order.Customer.User.Email,
                                    Email = productOrders[0].Order.Customer.User.Email,
                                    ProfilePicture = productOrders[0].Order.Customer.User.ProfilePicture
                                }
                            },
                            Quantity = x.Quantity,
                            OrderedDate = x.Order.CreatedAt.ToLongDateString(),
                            IsDelivered = productOrders[0].Order.IsDelivered,
                            Id = order.Id

                        }).ToList(),
                    };
                    allOrders.Add(orderProduct);
                }

                return new BaseResponse<IEnumerable<OrderProductDto>>
                {
                    Message = "Successful",
                    Status = true
                };
            }
            return new BaseResponse<IEnumerable<OrderProductDto>>
            {
                Message = "Failed",
                Status = false
            };
        }

        public async Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllOrdersAsync()
        {
            var orders = await _orderProductRepository.GetAllOrderAsync();
            if (orders.Count() == 0)
            {
                return new BaseResponse<IEnumerable<OrderProductDto>>
                {
                    Message = "Orders not found",
                    Status = false
                };
            }
            List<OrderProductDto> allOrders = new List<OrderProductDto>();
            foreach (var order in orders)
            {
                var orderProducts = await _orderProductRepository.GetAsync(x => x.Order.Id == order.Order.Id);

                var orderProduct = new OrderProductDto
                {
                    OrderDto = orderProducts.Select(x => new OrderDto
                    {
                        Product = new ProductDto
                        {
                            ProductName = x.Product.ProductName,
                            Price = x.Product.Price,
                            Image = x.Product.Image,
                            IsAvailable = x.Product.IsAvailable,
                            Description = x.Product.Description
                        },
                        Customer = new CustomerDto
                        {
                            Users = new UserDto
                            {
                                UserName = $"{orderProducts[0].Order.Customer.User.FirstName} {orderProducts[0].Order.Customer.User.LastName}",
                                PhoneNumber = orderProducts[0].Order.Customer.User.Email,
                                Email = orderProducts[0].Order.Customer.User.Email,
                                ProfilePicture = orderProducts[0].Order.Customer.User.ProfilePicture
                            }
                        },
                        Quantity = x.Quantity,
                        OrderedDate = x.Order.CreatedAt.ToLongDateString(),
                        IsDelivered = orderProducts[0].Order.IsDelivered,
                        Id = order.Id

                    }).ToList(),
                    AddressDto = new AddressDto
                    {
                        AddressId = order.Id,
                        State = orderProducts[0].Order.Address.State,
                        City = orderProducts[0].Order.Address.City,
                        Street = orderProducts[0].Order.Address.Street,
                        PostalCode = orderProducts[0].Order.Address.PostalCode,
                    },

                };
                allOrders.Add(orderProduct);
            }
            return new BaseResponse<IEnumerable<OrderProductDto>>
            {
                Message = "Orders found successfully",
                Status = true,
                Data = allOrders.Select(x => new OrderProductDto
                {
                    AddressDto = x.AddressDto,
                    OrderDto = x.OrderDto,
                    NetAmount = x.OrderDto.Sum(x => x.Quantity * x.Product.Price),
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<OrderProductDto>>> GetAllUnDeliveredOrdersAsync()
        {
            var orders = await _orderProductRepository.GetSelectedAsync( x => x.Order.IsDelivered == false);
            if (orders.Count() == 0)
            {
                return new BaseResponse<IEnumerable<OrderProductDto>>
                {
                    Message = "Orders not found",
                    Status = false
                };
            }
            List<OrderProductDto> allOrders = new List<OrderProductDto>();
            foreach (var order in orders)
            {
                var orderProducts = await _orderProductRepository.GetAsync(x => x.Order.Id == order.Order.Id);

                var orderProduct = new OrderProductDto
                {
                    OrderDto = orderProducts.Select(x => new OrderDto
                    {
                        Product = new ProductDto
                        {
                            ProductName = x.Product.ProductName,
                            Price = x.Product.Price,
                            Image = x.Product.Image,
                            IsAvailable = x.Product.IsAvailable,
                            Description = x.Product.Description
                        },
                        Customer = new CustomerDto
                        {
                            Users = new UserDto
                            {
                                UserName = $"{orderProducts[0].Order.Customer.User.FirstName} {orderProducts[0].Order.Customer.User.LastName}",
                                PhoneNumber = orderProducts[0].Order.Customer.User.Email,
                                Email = orderProducts[0].Order.Customer.User.Email,
                                ProfilePicture = orderProducts[0].Order.Customer.User.ProfilePicture
                            }
                        },
                        Quantity = x.Quantity,
                        OrderedDate = x.Order.CreatedAt.ToLongDateString(),
                        IsDelivered = orderProducts[0].Order.IsDelivered,
                        Id = order.Id

                    }).ToList(),
                    AddressDto = new AddressDto
                    {
                        AddressId = order.Id,
                        State = orderProducts[0].Order.Address.State,
                        City = orderProducts[0].Order.Address.City,
                        Street = orderProducts[0].Order.Address.Street,
                        PostalCode = orderProducts[0].Order.Address.PostalCode,
                    },

                };
                allOrders.Add(orderProduct);
            }
            return new BaseResponse<IEnumerable<OrderProductDto>>
            {
                Message = "Orders found successfully",
                Status = true,
                Data = allOrders.Select(x => new OrderProductDto
                {
                    AddressDto = x.AddressDto,
                    OrderDto = x.OrderDto,
                    NetAmount = x.OrderDto.Sum(x => x.Quantity * x.Product.Price),
                }).ToList()
            };
        }

        public Task<BaseResponse<OrderProductDto>> GetCart(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IList<OrderProductDto>>> GetOrderByIdAsync(string id)
        {
            var orders = await _orderProductRepository.GetSelectedAsync(x => x.Order.Id == id);
            if (orders.Count() == 0)
            {
                return new BaseResponse<IList<OrderProductDto>>
                {
                    Message = "Orders not found",
                    Status = false
                };
            }
            List<OrderProductDto> allOrders = new List<OrderProductDto>();
            foreach (var order in orders)
            {
                var orderProducts = await _orderProductRepository.GetAsync(x => x.Order.Id == order.Order.Id);

                var orderProduct = new OrderProductDto
                {
                    OrderDto = orderProducts.Select(x => new OrderDto
                    {
                        Product = new ProductDto
                        {
                            ProductName = x.Product.ProductName,
                            Price = x.Product.Price,
                            Image = x.Product.Image,
                            IsAvailable = x.Product.IsAvailable,
                            Description = x.Product.Description
                        },
                        Customer = new CustomerDto
                        {
                            Users = new UserDto
                            {
                                UserName = $"{orderProducts[0].Order.Customer.User.FirstName} {orderProducts[0].Order.Customer.User.LastName}",
                                PhoneNumber = orderProducts[0].Order.Customer.User.Email,
                                Email = orderProducts[0].Order.Customer.User.Email,
                                ProfilePicture = orderProducts[0].Order.Customer.User.ProfilePicture
                            }
                        },
                        Quantity = x.Quantity,
                        OrderedDate = x.Order.CreatedAt.ToLongDateString(),
                        IsDelivered = orderProducts[0].Order.IsDelivered,
                        Id = order.Id

                    }).ToList(),
                    AddressDto = new AddressDto
                    {
                        AddressId = order.Id,
                        State = orderProducts[0].Order.Address.State,
                        City = orderProducts[0].Order.Address.City,
                        Street = orderProducts[0].Order.Address.Street,
                        PostalCode = orderProducts[0].Order.Address.PostalCode,
                    },

                };
                allOrders.Add(orderProduct);
            }
            return new BaseResponse<IList<OrderProductDto>>
            {
                Message = "Orders found successfully",
                Status = true,
                Data = allOrders.Select(x => new OrderProductDto
                {
                    AddressDto = x.AddressDto,
                    OrderDto = x.OrderDto,
                    NetAmount = x.OrderDto.Sum(x => x.Quantity * x.Product.Price),
                }).ToList()
            };
        }

        public async Task<BaseResponse<IList<OrderProductDto>>> GetOrdersByCustomerIdAsync(string customerId)
        {
            var orders = await _orderProductRepository.GetSelectedAsync(x => x.Order.Customer.Id == customerId);
            if (orders.Count() == 0)
            {
                return new BaseResponse<IList<OrderProductDto>>
                {
                    Message = "Orders not found",
                    Status = false
                };
            }
            List<OrderProductDto> allOrders = new List<OrderProductDto>();
            foreach (var order in orders)
            {
                var orderProducts = await _orderProductRepository.GetAsync(x => x.Order.Id == order.Order.Id);

                var orderProduct = new OrderProductDto
                {
                    OrderDto = orderProducts.Select(x => new OrderDto
                    {
                        Product = new ProductDto
                        {
                            ProductName = x.Product.ProductName,
                            Price = x.Product.Price,
                            Image = x.Product.Image,
                            IsAvailable = x.Product.IsAvailable,
                            Description = x.Product.Description
                        },
                        Customer = new CustomerDto
                        {
                            Users = new UserDto
                            {
                                UserName = $"{orderProducts[0].Order.Customer.User.FirstName} {orderProducts[0].Order.Customer.User.LastName}",
                                PhoneNumber = orderProducts[0].Order.Customer.User.Email,
                                Email = orderProducts[0].Order.Customer.User.Email,
                                ProfilePicture = orderProducts[0].Order.Customer.User.ProfilePicture
                            }
                        },
                        Quantity = x.Quantity,
                        OrderedDate = x.Order.CreatedAt.ToLongDateString(),
                        IsDelivered = orderProducts[0].Order.IsDelivered,
                        Id = order.Id

                    }).ToList(),
                    AddressDto = new AddressDto
                    {
                        AddressId = order.Id,
                        State = orderProducts[0].Order.Address.State,
                        City = orderProducts[0].Order.Address.City,
                        Street = orderProducts[0].Order.Address.Street,
                        PostalCode = orderProducts[0].Order.Address.PostalCode,
                    },

                };
                allOrders.Add(orderProduct);
            }
            return new BaseResponse<IList<OrderProductDto>>
            {
                Message = "Orders found successfully",
                Status = true,
                Data = allOrders.Select(x => new OrderProductDto
                {
                    AddressDto = x.AddressDto,
                    OrderDto = x.OrderDto,
                    NetAmount = x.OrderDto.Sum(x => x.Quantity * x.Product.Price),
                }).ToList()
            };
        }

        public Task<BaseResponse<OrderDto>> UpdateOrderAsync(string id, UpdateOrderRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}