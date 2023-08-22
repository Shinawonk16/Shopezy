using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface IBrandService
{
    Task<BaseResponse<BrandsDto>> AddBrandAsync(CreateBrandsRequestModel model);
    Task<BaseResponse<IList<BrandsDto>>> GetAllBrand();
    Task<BaseResponse<BrandsDto>> GetById( string id);
    Task<BaseResponse<BrandsDto>> UpdateBrandAsync(string id,UpdateBrandsRequestModel model);


}
