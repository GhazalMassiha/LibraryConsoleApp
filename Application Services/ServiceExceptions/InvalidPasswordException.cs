

namespace HW12_Issue1.Application_Services.ServiceExceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() { }

        public InvalidPasswordException(string message) : base(message) { }
    }
}
