namespace CreditoApp.Domain.Exceptions
{
    public class AuthException : CustomException
    {
        public AuthException(string message = "Authentication Error", int code = 401) : base(message, code) { }
    }
}
