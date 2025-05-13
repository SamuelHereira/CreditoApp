using CreditoApp.Domain.Entitites.CreditApp;

namespace CreditoApp.Infrastructure.Interfaces.Repositories
{
    public interface ICreditRequestRepository
    {
        Task<CreditRequest> CreateCreditRequest(CreditRequest creditRequest);
        Task<List<CreditRequest>> GetCreditRequestsByUserId(int userId);

        Task<CreditRequest> DeleteCreditRequest(int requestId);
    }

}