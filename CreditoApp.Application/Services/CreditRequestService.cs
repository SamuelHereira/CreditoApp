
using CreditoApp.Application.Interfaces.Services;
using CreditoApp.Domain.Entitites.CreditApp;
using CreditoApp.Domain.Enums;
using CreditoApp.Domain.Models.Requests.CreditApp;
using CreditoApp.Domain.Models.Responses.CreditApp;
using CreditoApp.Infrastructure.Interfaces.Repositories;

namespace CreditoApp.Application.Services
{
    public class CreditRequestService : ICreditRequestService
    {
        private readonly ICreditRequestRepository _creditRequestRepository;

        public CreditRequestService(ICreditRequestRepository creditRequestRepository)
        {
            _creditRequestRepository = creditRequestRepository;
        }

        public async Task<CreditRequestResponse> CreateCreditRequest(CreateCreditRequest request)
        {

            var creditRequest = new CreditRequest
            {
                UserId = request.UserId,
                Amount = request.Amount,
                Status = CreditStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
            var createdCreditRequest = await _creditRequestRepository.CreateCreditRequest(creditRequest);

            return new CreditRequestResponse
            {
                Id = createdCreditRequest.Id,
                UserId = createdCreditRequest.UserId,
                Amount = createdCreditRequest.Amount,
                TermMonths = createdCreditRequest.TermMonths,
                MonthlyIncome = createdCreditRequest.MonthlyIncome,
                JobSeniorityYears = createdCreditRequest.JobSeniorityYears,
                Status = createdCreditRequest.Status.ToString(),
            };
        }

        public async Task<List<CreditRequestResponse>> GetCreditRequestsByUserId(int userId)
        {
            var creditRequests = await _creditRequestRepository.GetCreditRequestsByUserId(userId);

            if (creditRequests == null || creditRequests.Count == 0)
            {
                return new List<CreditRequestResponse>();
            }

            return creditRequests.Select(cr => new CreditRequestResponse
            {
                Id = cr.Id,
                UserId = cr.UserId,
                Amount = cr.Amount,
                TermMonths = cr.TermMonths,
                MonthlyIncome = cr.MonthlyIncome,
                JobSeniorityYears = cr.JobSeniorityYears,
                Status = cr.Status.ToString(),
            }).ToList();
        }
    }
}