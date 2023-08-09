using Application.Abstractions.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository;

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<IEnumerable<Payment>> GetAllPaymentAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Payment> GetPaymentAsync(string referenceNumber)
    {
        return await _context.Payments
            .Include(x => x.Order)
            .Include(x => x.Customer)
            .ThenInclude(c => c.User)
            .Where(x => x.ReferenceNumber == referenceNumber)
            .SingleOrDefaultAsync();
        
    }
}
