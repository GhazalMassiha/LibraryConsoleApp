

namespace HW12_Issue1.Application_Services.ServiceExceptions
{
    internal class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException() { }

        public CategoryNotFoundException(string message) : base(message) { }
    }
}
