using Application.Abstractions.IRepositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repository;

public class VerificationRepository : BaseRepository<Verification>, IVerificationRepository
{
    public VerificationRepository(ApplicationDbContext context) : base(context)
    {
    }
}
