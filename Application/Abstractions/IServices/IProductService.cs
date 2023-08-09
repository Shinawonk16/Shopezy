using Domain.Entities;
using Application.Dtos;
namespace Application.Abstractions.IServices;

public interface IProductService
{
    Task<BaseResponse<ProductDto>> CreateProductAsync(AddProductRequestModel model);
    Task<BaseResponse<ProductDto>> UpdateProductAsync(UpdateProductRequestModel model);
    Task<BaseResponse<ProductDto>> GetAsync(string id);
    Task<BaseResponse<ProductDto>> GetByCategoryAsync(string categoryId);
    Task<BaseResponse<ProductDto>> GetAvailableProductsAsync();
    Task<BaseResponse<IEnumerable<ProductDto>>> GetAllAsync();

}
