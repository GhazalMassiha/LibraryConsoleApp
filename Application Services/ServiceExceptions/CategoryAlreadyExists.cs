
namespace HW12_Issue1.Application_Services.ServiceExceptions
{
    internal class CategoryAlreadyExists : Exception
    {
        public CategoryAlreadyExists() { }

        public CategoryAlreadyExists(string message) : base(message) { }
    }
}
