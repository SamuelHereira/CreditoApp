using CreditoApp.Domain.Entitites.CreditApp;
using CreditoApp.Infrastructure.Database;
using CreditoApp.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CreditoApp.Infrastructure.Repositories
{

    public class CreditRequestRepository : ICreditRequestRepository
    {

        private readonly DatabaseContext _context;

        public CreditRequestRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<CreditRequest> CreateCreditRequest(CreditRequest creditRequest)
        {
            await _context.CreditRequests.AddAsync(creditRequest);
            await _context.SaveChangesAsync();
            return creditRequest;
        }

        public async Task<List<CreditRequest>> GetCreditRequestsByUserId(int userId)
        {
            var creditRequests = await _context.CreditRequests
                .Where(cr => cr.UserId == userId)
                .ToListAsync();

            if (creditRequests == null || creditRequests.Count == 0)
            {
                return new List<CreditRequest>();
            }

            return creditRequests;
        }
    }
}
