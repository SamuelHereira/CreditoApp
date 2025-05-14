using CreditoApp.Application.Interfaces.Services;
using CreditoApp.Domain.Enums;
using CreditoApp.Domain.Models.Responses.CreditApp;
using CreditoApp.Infrastructure.Interfaces.Repositories;
using CreditoApp.Domain.Exceptions;


namespace CreditoApp.Application.Services
{
    public class CreditReviewService : ICreditReviewService
    {
        private readonly ICreditReviewRepository _creditReviewRepository;
        private readonly AuditLogger _auditLogger;

        public CreditReviewService(ICreditReviewRepository creditReviewRepository, AuditLogger auditLogger)
        {
            _creditReviewRepository = creditReviewRepository;
        }

        public async Task<List<CreditRequestResponse>> GetCreditRequests(int? status = null)
        {
            var creditRequests = await _creditReviewRepository.GetCreditRequests(status);
            return creditRequests.Select(cr => new CreditRequestResponse
            {
                Id = cr.Id,
                UserId = cr.UserId,
                Amount = cr.Amount,
                Status = cr.Status.ToString(),
                JobSeniorityYears = cr.JobSeniorityYears,
                MonthlyIncome = cr.MonthlyIncome,
                TermMonths = cr.TermMonths,
                RequestDate = cr.RequestDate,
                User = cr.User,
                UpdateDate = cr.UpdatedAt
            }).ToList();
        }

        public async Task<CreditRequestResponse> GetCreditRequestById(int requestId)
        {
            var creditRequest = await _creditReviewRepository.GetCreditRequestById(requestId);
            if (creditRequest == null)
            {
                throw new ClientFaultException();
            }
            return new CreditRequestResponse
            {
                Id = creditRequest.Id,
                UserId = creditRequest.UserId,
                Amount = creditRequest.Amount,
                Status = creditRequest.Status.ToString(),
                JobSeniorityYears = creditRequest.JobSeniorityYears,
                MonthlyIncome = creditRequest.MonthlyIncome,
                TermMonths = creditRequest.TermMonths,
                RequestDate = creditRequest.RequestDate,
                User = creditRequest.User,
                UpdateDate = creditRequest.UpdatedAt
            };
        }

        public async Task<CreditRequestResponse> UpdateCreditRequestStatus(int requestId, string status)
        {

            var newStatus = Enum.TryParse<CreditStatus>(status, true, out var creditStatus);

            var creditRequest = await _creditReviewRepository.UpdateCreditRequestStatus(requestId, newStatus ? creditStatus : CreditStatus.Pending);
            if (creditRequest == null)
            {
                throw new ClientFaultException("Credit request not found");
            }

            if (creditRequest.Status == CreditStatus.Approved)
            {
                await _auditLogger.Log("Update", "CreditRequest", $"Credit request approved: {creditRequest.Id}");
            }


            if (creditRequest.Status == CreditStatus.Rejected)
            {
                await _auditLogger.Log("Update", "CreditRequest", $"Credit request rejected: {creditRequest.Id}");
            }


            return new CreditRequestResponse
            {
                Id = creditRequest.Id,
                UserId = creditRequest.UserId,
                Amount = creditRequest.Amount,
                Status = creditRequest.Status.ToString(),
                JobSeniorityYears = creditRequest.JobSeniorityYears,
                MonthlyIncome = creditRequest.MonthlyIncome,
                TermMonths = creditRequest.TermMonths,
            };
        }
    }
}