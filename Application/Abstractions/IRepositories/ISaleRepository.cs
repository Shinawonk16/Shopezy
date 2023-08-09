using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Abstractions.IRepositories;

public interface ISaleRepository : IBaseRepository<Sales>
{
    Task<Sales> GetSaleAsync(string id);
    Task<IEnumerable<Sales>> GetSaleByCustomerIdAsync(string customerId);
    Task<IEnumerable<Sales>> GetAllSalesOfTheYearAsync();
    Task<IEnumerable<Sales>> GetAllSalesOfTheMonthAsync();
    Task<IEnumerable<Sales>> GetAllSaleAsync();
    Task<Sales> GetSaleAsync(Expression<Func<Sales, bool>> expression);
    Task<IEnumerable<Sales>> GetAllSaleAsync(Expression<Func<Sales, bool>> expression);
    Task<decimal> GetTotalMonthlySalesAsync();
    Task<decimal> GetTotalYearlySalesAsync();
    Task<decimal> GetTotalMonthlySalesAsync(int month, int year);
    Task<decimal> GetTotalYearlySalesAsync(int year);
}
