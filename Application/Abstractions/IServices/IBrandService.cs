using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface IBrandService
{
    Task<BaseResponse<BrandsDto>> AddBrandAsync(CreateBrandsRequestModel model);
    Task<BaseResponse<BrandsDto>> UpdateBrandAsync(string id,UpdateBrandsRequestModel model);


}
