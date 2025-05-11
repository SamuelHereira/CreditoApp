using CreditoApp.Domain.Models.Responses.CreditApp;
using CreditoApp.Domain.Models.Responses.Shared;

namespace CreditoApp.Application.Interfaces.Services
{
    public interface ICreditReviewService
    {
        Task<List<CreditRequestResponse>> GetCreditRequests();
        Task<CreditRequestResponse> GetCreditRequestById(int requestId);
        Task<CreditRequestResponse> UpdateCreditRequestStatus(int requestId, string status);
    }
}