namespace CreditoApp.Domain.Exceptions
{
    public class ServerFaultException : CustomException
    {
        public ServerFaultException(string message = "Internal Server Error", int code = 500) : base(message, code) { }
    }
}
