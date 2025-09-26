
using HW12_Issue1.Domain.Entities;

namespace HW12_Issue1.Interfaces.Repository_Contracts
{
    public interface IBorrowedBookRepository
    {
        public List<BorrowedBook> GetByUserId(int userId);
        public void Add(BorrowedBook borrowedBook);
        public void Delete(int id);
    }
}
