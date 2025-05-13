using CreditoApp.Domain.Models.Requests.CreditApp;
using CreditoApp.Domain.Models.Responses.CreditApp;
using CreditoApp.Domain.Models.Responses.Shared;

namespace CreditoApp.Application.Interfaces.Services
{
    public interface ICreditRequestService
    {
        Task<CreditRequestResponse> CreateCreditRequest(CreateCreditRequest request);
        Task<List<CreditRequestResponse>> GetCreditRequestsByUserId(int userId);

        Task<CreditRequestResponse> DeleteCreditRequest(int requestId);

    }
}