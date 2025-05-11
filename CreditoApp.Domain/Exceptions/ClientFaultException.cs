namespace CreditoApp.Domain.Exceptions
{
    public class ClientFaultException : CustomException
    {
        public ClientFaultException(string message = "Bad Request", int code = 400) : base(message, code) { }
    }
}
