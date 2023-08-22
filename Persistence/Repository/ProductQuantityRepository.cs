using Application.Abstractions.IRepositories;
using Domain.Entities;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public class ProductQuantityRepository : BaseRepository<ProductQuantity>, IProductQuantityRepository
    {
        public ProductQuantityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddRestockEvent(string productQuantityId, RestockEvent restockEvent)
        {
            var productQuantity = await _context.ProductQuantities

       .Include(pq => pq.RestockEvents)
       .SingleOrDefaultAsync(pq => pq.Id == productQuantityId);

            // if (productQuantity != null)
            // {
            //     productQuantity.RestockEvents.Add(restockEvent);
            //     // await _dbContext.SaveChangesAsync();
            // }

        }

        public async Task<IList<ProductQuantity>> GetProductQuantities()
        {
            return await _context.ProductQuantities
            .Include(pq => pq.SalesHistory)
            .Include(pq => pq.RestockEvents)
            .ToListAsync();
        }

        public async Task<ProductQuantity> GetProductQuantityById(string productQuantityId)
        {
            return await _context.ProductQuantities
            .Include(pq => pq.SalesHistory)
            .Include(pq => pq.RestockEvents)
            .SingleOrDefaultAsync(pq => pq.Id == productQuantityId);
        }
    }
}