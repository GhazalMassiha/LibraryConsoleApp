using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures.LContext;
using HW12_Issue1.Interfaces.Repository_Contracts;

namespace HW12_Issue1.Infrastructures.Repositories
{
    public class BookRepository : IBookRepository
    {
        private LibraryContext _Lc = new LibraryContext();

        public Book? GetById(int id)
        {
            return _Lc.Books.SingleOrDefault(b => b.Id == id);
        }


        public List<Book> GetAll()
        {
            return _Lc.Books.ToList();
        }

        public List<Book> GetByCategoryId(int categoryId)
        {
            return _Lc.Books.Where(b => b.CategoryId == categoryId).ToList();
        }

        public void Add(Book book)
        {
            _Lc.Books.Add(book);
            _Lc.SaveChanges();
        }

        public void Update(Book book)
        {
            _Lc.Books.Update(book);
            _Lc.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = _Lc.Books.SingleOrDefault(x => x.Id == id);
            if (book != null)
            {
                _Lc.Books.Remove(book);
                _Lc.SaveChanges();
            }
        }
    }
}
