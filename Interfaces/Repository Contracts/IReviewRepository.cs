

using HW12_Issue1.Domain.Entities;

namespace HW12_Issue1.Interfaces.Repository_Contracts
{
    public interface IReviewRepository
    {
        public List<Review> GetAll();
        public List<Review> GetByBookId(int bookId);
        public List<Review> GetByUserId(int userId);
        public Review? GetById(int id);
        public void Add(Review review);
        public void Update(Review review);
        public void Delete(int id);
    }
}
