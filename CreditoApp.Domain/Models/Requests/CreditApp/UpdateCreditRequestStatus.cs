using CreditoApp.Domain.Enums;

namespace CreditoApp.Domain.Models.Requests.CreditApp
{
    public class UpdateCreditRequestStatus
    {
        public int CreditRequestId { get; set; }
        public CreditStatus NewStatus { get; set; }
    }
}