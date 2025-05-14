using CreditoApp.Domain.Entitites.CreditApp;
using CreditoApp.Domain.Enums;
using CreditoApp.Domain.Models.Requests.CreditApp;
using CreditoApp.Infrastructure.Database;
using CreditoApp.Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CreditoApp.Infrastructure.Repositories
{

    public class CreditReviewRepository : ICreditReviewRepository
    {
        private readonly DatabaseContext _context;

        public CreditReviewRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<CreditRequest>> GetCreditRequests(
            int? status = null
        )
        {
            return await _context.CreditRequests
                .Include(cr => cr.User)
                .Where(cr => status == null || cr.Status == (CreditStatus)status)
                .ToListAsync();
        }

        public async Task<CreditRequest> GetCreditRequestById(int requestId)
        {
            CreditRequest creditRequest = await _context.CreditRequests
               .Include(cr => cr.User)
               .FirstOrDefaultAsync(cr => cr.Id == requestId);

            if (creditRequest == null)
            {
                throw new Exception("Credit request not found");
            }

            return creditRequest;

        }

        public async Task<CreditRequest> UpdateCreditRequestStatus(int requestId, CreditStatus status)
        {
            var creditRequest = await _context.CreditRequests.FindAsync(requestId);

            if (creditRequest == null)
            {
                throw new Exception("Credit request not found");
            }

            creditRequest.Status = status;
            await _context.SaveChangesAsync();

            return creditRequest;
        }
    }
}