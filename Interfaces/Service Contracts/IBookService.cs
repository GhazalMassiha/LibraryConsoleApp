

using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures.Repositories;

namespace HW12_Issue1.Interfaces.Service_Contracts
{
    public interface IBookService
    {
        public void ViewAllBooks();
        public void ViewAllCategories();
    }
}
