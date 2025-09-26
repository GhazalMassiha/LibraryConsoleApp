

namespace HW12_Issue1.Application_Services.ServiceExceptions
{
    internal class ReviewNotFoundException : Exception
    {
        public ReviewNotFoundException() { }

        public ReviewNotFoundException(string message) : base(message) { }
    }
}
