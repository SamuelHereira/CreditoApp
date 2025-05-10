namespace CreditoApp.Domain.Models.Responses.Shared
{
    public class SuccessResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public SuccessResponse(int code, string message, T data) : base(code, message)
        {
            Data = data;
        }
    }
}
