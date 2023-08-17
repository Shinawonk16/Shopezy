using Application.Abstractions.IServices;
using Application.Dtos;

namespace Application.Services;

public class BrandService : IBrandService
{
    public Task<BaseResponse<BrandsDto>> AddBrandAsync(CreateBrandsRequestModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<BrandsDto>> UpdateBrandAsync(string id, UpdateBrandsRequestModel model)
    {
        throw new NotImplementedException();
    }
}
