

namespace HW12_Issue1.Application_Services.ServiceExceptions
{
    internal class BookNotFoundException : Exception
    {
        public BookNotFoundException() { }

        public BookNotFoundException(string message) : base(message) { }
    }
}
