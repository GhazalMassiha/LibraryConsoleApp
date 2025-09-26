

using HW12_Issue1.Domain.Entities;

namespace HW12_Issue1.Interfaces.Repository_Contracts
{
    public interface IBookRepository
    {
        public Book? GetById(int id);
        public List<Book> GetAll();
        public List<Book> GetByCategoryId(int categoryId);
        public void Add(Book book);
        public void Update(Book book);
        public void Delete(int id);
    }
}
