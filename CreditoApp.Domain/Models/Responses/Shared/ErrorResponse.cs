namespace CreditoApp.Domain.Models.Responses.Shared
{
    public class ErrorResponse : BaseResponse
    {
        public ErrorResponse(int code, string message, Error error) : base(code, message)
        {
            Error = error;
        }

        public Error Error { get; set; }

    }
    public struct Error
    {
        public int Code { get; set; }
        public String ErrorMessage { get; set; }
    }
}
