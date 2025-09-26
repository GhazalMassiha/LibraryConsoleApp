

using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures.LContext;
using HW12_Issue1.Interfaces.Repository_Contracts;

namespace HW12_Issue1.Infrastructures.Repositories
{
    public class BorrowedBookRepository : IBorrowedBookRepository
    {
        private LibraryContext _Lc = new LibraryContext();

        public List<BorrowedBook> GetByUserId(int userId)
        {
            return _Lc.BorrowedBooks.Where(bb => bb.UserId == userId).ToList();
        }

        public void Add(BorrowedBook borrowedBook)
        {
            _Lc.BorrowedBooks.Add(borrowedBook);
            _Lc.SaveChanges();
        }

        public void Delete(int id)
        {
            var borrowedBook = _Lc.BorrowedBooks.SingleOrDefault(x => x.Id == id);
            if (borrowedBook != null)
            {
                _Lc.BorrowedBooks.Remove(borrowedBook);
                _Lc.SaveChanges();
            }
        }
    }
}
