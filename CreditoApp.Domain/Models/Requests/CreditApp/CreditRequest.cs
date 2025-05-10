namespace CreditoApp.Domain.Models.Requests.CreditApp
{
    public class CreditRequest
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int TermMonths { get; set; }
        public decimal MonthlyIncome { get; set; }
        public int JobSeniorityYears { get; set; }
    }
}