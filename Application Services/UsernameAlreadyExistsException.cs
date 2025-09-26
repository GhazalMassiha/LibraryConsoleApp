

namespace HW12_Issue1.Application_Services
{
    public class UsernameAlreadyExistsException : Exception
    {
        public UsernameAlreadyExistsException() { }

        public UsernameAlreadyExistsException(string message) : base(message) { }
    }
}
