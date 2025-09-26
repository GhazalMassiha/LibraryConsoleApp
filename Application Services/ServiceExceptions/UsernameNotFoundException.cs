

namespace HW12_Issue1.Application_Services.ServiceExceptions
{
    public class UsernameNotFoundException : Exception
    {
        public UsernameNotFoundException() { }

        public UsernameNotFoundException(string message) : base(message) { }
    }
}
