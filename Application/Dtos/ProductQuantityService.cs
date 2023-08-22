using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Domain.Entities;

namespace Application.Dtos;

public class ProductQuantityService : IProductQuantityService
{
    private readonly IProductQuantityRepository _productQuantityRepository;

    public ProductQuantityService(IProductQuantityRepository productQuantityRepository)
    {
        _productQuantityRepository = productQuantityRepository;
    }
    public async Task AddRestockEventAsync(string productQuantityId, RestockEvent restockEvent)
    {
        var get = await _productQuantityRepository.GetProductQuantityById(productQuantityId);
        if (get != null)
        {
            await _productQuantityRepository.AddRestockEvent(productQuantityId, restockEvent);
        }
    }

    public async Task<BaseResponse<IList<ProductQuantityDto>>> GetProductQuantities()
    {
        var getAll = await _productQuantityRepository.GetProductQuantities();
        if (getAll != null)
        {
            return new BaseResponse<IList<ProductQuantityDto>>
            {
                // Data = getAll.Select(x => new ProductQuantityDto
                // {
                //     ProductDto = new ProductDto
                //     {
                //         ProductName = x.Product.ProductName,
                //         Price = x.Product.Price,
                //         Description = x.Product.Description,
                //         Image = x.Product.Image
                //     },
                //     RestockEventDto = x.RestockEvents

                // }).ToList()
                Message = "Successful",
                Status = true

            };
        }
        return new BaseResponse<IList<ProductQuantityDto>>
        {
            Message = "Falied",
            Status = false
        };
    }

    public async Task<BaseResponse<ProductQuantityDto>> GetProductQuantityByIdAsync(string productQuantityId)
    {
        var get = await _productQuantityRepository.GetProductQuantityById(productQuantityId);
        if (get != null)
        {
            return new BaseResponse<ProductQuantityDto>
            {
                Data = new ProductQuantityDto
                {
                    ProductDto = new ProductDto
                    {
                        ProductName = get.Product.ProductName,
                        Price = get.Product.Price,
                        Description = get.Product.Description,
                        Image = get.Product.Image
                    },
                    RestockEventDto = new RestockEventDto
                    {
                        RestockDate = get.RestockEvents.CreatedAt,
                        RestockedBy = get.RestockEvents.CreatedBy,
                        QuantityAdded = get.RestockEvents.QuantityAdded,
                        Notes = get.RestockEvents.Notes,
                    },
                    // SalesDto = new SalesDto{}.
                }

            };
        }
        return new BaseResponse<ProductQuantityDto>
        {
            Message = "Falied",
            Status = false
        };
    }

    public Task UpdateRestockEventAsync(string productQuantityId, RestockEvent restockEvent)
    {
        throw new NotImplementedException();
    }
}
