using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures.LContext;
using HW12_Issue1.Interfaces.Repository_Contracts;
using Microsoft.EntityFrameworkCore;


namespace HW12_Issue1.Infrastructures.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private LibraryContext _Lc = new LibraryContext();

        public List<Review> GetAll()
        {
            return _Lc.Reviews.ToList();
        }

        public List<Review> GetByBookId(int bookId)
        {
            return _Lc.Reviews.Where(r => r.BookId == bookId).ToList();
        }

        public List<Review> GetByUserId(int userId)
        {
            return _Lc.Reviews.Where(r => r.UserId == userId).ToList();
        }

        public Review? GetById(int id)
        {
            return _Lc.Reviews.SingleOrDefault(r => r.Id == id);
        }

        public void Add(Review review)
        {
            _Lc.Reviews.Add(review);
            _Lc.SaveChanges();
        }

        public void Update(Review review)
        {
            _Lc.Reviews.Update(review);
            _Lc.SaveChanges();
        }

        public void Delete(int id)
        {
            var review = _Lc.Reviews.SingleOrDefault(r => r.Id == id);
            if (review != null)
            {
                _Lc.Reviews.Remove(review);
                _Lc.SaveChanges();
            }
        }
    }
}
