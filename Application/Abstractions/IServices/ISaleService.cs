using Application.Dtos;

namespace Application.Abstractions.IServices;

public interface ISaleService
{

    Task<BaseResponse<SalesDto>> CalculateAllMonthlySalesAsync(int year);
    Task<BaseResponse<ProfitDto>> CalculateMonthlyProfitAsync(int month, int year);
    Task<BaseResponse<ProfitDto>> CalculateNetProfitAsync(int year, int month, decimal extraExpenses);
    Task<BaseResponse<ProfitDto>> CalculateThisMonthProfitAsync();
    Task<BaseResponse<ProfitDto>> CalculateThisYearProfitAsync();
    Task<BaseResponse<ProfitDto>> CalculateYearlyProfitAsync(int year);
    Task<BaseResponse<SalesDto>> CreateSales(string id);
    Task<BaseResponse<SalesDto>> GetAllSales();
    Task<BaseResponse<SalesDto>> GetSalesByCustomerNameAsync(string name);
    Task<BaseResponse<SalesDto>> GetSalesByProductNameForTheMonth(string productId, int month, int year);
    Task<BaseResponse<SalesDto>> GetSalesByProductNameForTheYear(string productId, int year);
    Task<BaseResponse<SalesDto>> GetSalesForTheMonthOnEachProduct(int month, int year);
    Task<BaseResponse<SalesDto>> GetSalesForTheYearOnEachProduct(int year);
    Task<BaseResponse<SalesDto>> GetSalesForThisMonth();
    Task<BaseResponse<SalesDto>> GetSalesForThisYear();
}
