
using CreditoApp.Application.Interfaces.Services;
using CreditoApp.Domain.Entitites.CreditApp;
using CreditoApp.Domain.Enums;
using CreditoApp.Domain.Exceptions;
using CreditoApp.Domain.Models.Requests.CreditApp;
using CreditoApp.Domain.Models.Responses.CreditApp;
using CreditoApp.Infrastructure.Interfaces.Repositories;

namespace CreditoApp.Application.Services
{
    public class CreditRequestService : ICreditRequestService
    {
        private readonly ICreditRequestRepository _creditRequestRepository;
        private readonly AuditLogger _auditLogger;

        public CreditRequestService(ICreditRequestRepository creditRequestRepository, AuditLogger auditLogger)
        {
            _creditRequestRepository = creditRequestRepository;
            _auditLogger = auditLogger;
        }

        public async Task<CreditRequestResponse> CreateCreditRequest(CreateCreditRequest request)
        {

            var creditRequest = new CreditRequest
            {
                UserId = request.UserId,
                Amount = request.Amount,
                TermMonths = request.TermMonths,
                MonthlyIncome = request.MonthlyIncome,
                JobSeniorityYears = request.JobSeniorityYears,
                Status = CreditStatus.Pending,
                RequestDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            var createdCreditRequest = await _creditRequestRepository.CreateCreditRequest(creditRequest);

            await _auditLogger.Log(
                "Create",
                "CreditRequest",
                $"Credit request created for user {request.UserId} with amount {request.Amount} and term {request.TermMonths} months."
            );

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
                RequestDate = cr.RequestDate,
                Status = cr.Status.ToString(),
            }).ToList();
        }

        public async Task<CreditRequestResponse> DeleteCreditRequest(int requestId)
        {
            var creditRequest = await _creditRequestRepository.DeleteCreditRequest(requestId);
            if (creditRequest == null)
            {
                throw new ClientFaultException("Credit request not found");
            }

            await _auditLogger.Log(
                "Delete",
                "CreditRequest",
                $"Credit request deleted: {creditRequest.Id}"
            );

            return new CreditRequestResponse
            {
                Id = creditRequest.Id,
                UserId = creditRequest.UserId,
                Amount = creditRequest.Amount,
                TermMonths = creditRequest.TermMonths,
                MonthlyIncome = creditRequest.MonthlyIncome,
                JobSeniorityYears = creditRequest.JobSeniorityYears,
                Status = creditRequest.Status.ToString(),
            };
        }
    }
}