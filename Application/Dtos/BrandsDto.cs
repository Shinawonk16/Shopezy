namespace Application.Dtos;

public class BrandsDto
{
    
        public string BrandName{get;set;}
        public string BrandDescription{get;set;}
        public IEnumerable<ProductDto> ProductDtos{get;set;}
    
}
public class CreateBrandsRequestModel
    {
        public string BrandName{get;set;}
        public string BrandDescription{get;set;}
    }
    
    public class UpdateBrandsRequestModel:CreateBrandsRequestModel
    {
    }
