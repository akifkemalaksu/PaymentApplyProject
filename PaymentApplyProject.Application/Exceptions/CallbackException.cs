namespace PaymentApplyProject.Application.Exceptions
{
    public class CallbackException : Exception
    {
        public string ErrorCode { get; set; }
        public CallbackException(string? message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
