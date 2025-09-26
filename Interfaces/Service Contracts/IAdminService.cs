

using HW12_Issue1.Domain.Entities;

namespace HW12_Issue1.Interfaces.Service_Contracts
{
    public interface IAdminService
    {
        public void AddBook(string title, int categoryId);
        public void AddCategory(string name);
        public void DeleteBook(int bookId);
        public void DeleteCategory(int categoryId);
        public void DeleteReview(int reviewId);
        public void ViewAllReviews();
    }
}
