using Domain.Entities;
namespace Application.Abstractions.IRepositories;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<Payment> GetPaymentAsync(string referenceNumber);
    Task<IEnumerable<Payment>> GetAllPaymentAsync();
}
