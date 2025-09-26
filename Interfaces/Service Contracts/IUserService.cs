

using HW12_Issue1.Application_Services.ServiceExceptions;
using HW12_Issue1.Domain.Entities;

namespace HW12_Issue1.Interfaces.Service_Contracts
{
    public interface IUserService
    {
        public void ViewMyBorrowedBooks(User user);
        public bool BorrowBook(User user, int bookId);
        public bool ReturnBook(User user, int borrowedId);
        public void ViewMyReviews(User user);
        public void AddReview(User user, int bookId, string? comment, int rating);
        public void EditReview(User user, int reviewId, string? newComment, int newRating);
        public void DeleteReview(User user, int reviewId);
    }
}
