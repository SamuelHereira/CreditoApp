using CreditoApp.Domain.Entitites.CreditApp;
using CreditoApp.Domain.Enums;

namespace CreditoApp.Infrastructure.Interfaces.Repositories
{
    public interface ICreditReviewRepository
    {
        Task<List<CreditRequest>> GetCreditRequests(
            int? status = null
        );
        Task<CreditRequest> GetCreditRequestById(int requestId);
        Task<CreditRequest> UpdateCreditRequestStatus(int requestId, CreditStatus status);
    }

}