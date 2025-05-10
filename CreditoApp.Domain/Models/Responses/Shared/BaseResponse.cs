
namespace CreditoApp.Domain.Models.Responses.Shared
{
    public abstract class BaseResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        protected BaseResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
