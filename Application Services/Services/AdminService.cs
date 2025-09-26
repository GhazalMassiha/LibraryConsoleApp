

using ConsoleTables;
using HW12_Issue1.Application_Services.ServiceExceptions;
using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures.Repositories;
using HW12_Issue1.Interfaces.Service_Contracts;

namespace HW12_Issue1.Application_Services.Services
{
    public class AdminService : IAdminService
    {
        private CategoryRepository _categoryRepo = new CategoryRepository();
        private BookRepository _bookRepo = new BookRepository(); 
        private ReviewRepository _reviewRepo = new ReviewRepository();


        public void AddBook(string title, int categoryId)
        {
            var book = new Book { Title = title, CategoryId = categoryId, IsBorrowed = false };
            _bookRepo.Add(book);
        }

        public void AddCategory(string name)
        {
            IsCategoryAvailable(name);

            var category = new Category { Name = name };
            _categoryRepo.Add(category);
        }

        public void DeleteBook(int bookId)
        {
            IsBookIdValid(bookId);

            _bookRepo.Delete(bookId);
        }

        public void DeleteCategory(int categoryId)
        {
            IsCategoryIdValid(categoryId);

            _categoryRepo.Delete(categoryId);
        }

        public void DeleteReview(int reviewId)
        {
            IsReviewIdValid(reviewId);

            _reviewRepo.Delete(reviewId);
        }

        public void ViewAllReviews()
        {
            var reviews = _reviewRepo.GetAll();
            if (reviews == null || reviews.Count == 0)
            {
                Console.WriteLine("\nNo Reviews Found.\n");
                return;
            }

            var table = new ConsoleTable("ReviewID", "BookID", "UserID", "Rating", "Comment", "CreatedAt");
            foreach (var r in reviews)
            {
                table.AddRow(r.Id, r.BookId, r.UserId, r.Rating, r.Comment ?? "", r.CreatedAt.ToString("g"));
            }
            table.Write();
            Console.WriteLine();
        }


        // --- Helper Methods ---
        private void IsCategoryAvailable (string name)
        {
            if (_categoryRepo.GetByName(name) != null)
            {
                throw new CategoryAlreadyExists("\nCategory Already Exists.");
            }      
        }

        private void IsBookIdValid (int bookId)
        {
            if (_bookRepo.GetById(bookId) == null)
            {
                throw new BookNotFoundException("\nBook Not Found.");
            }
        }

        private void IsCategoryIdValid (int categoryId)
        {
            if (_categoryRepo.GetById(categoryId) == null)
            {
                throw new CategoryNotFoundException("\nCategory Not Found.");
            }
        }

        private void IsReviewIdValid(int reviewId)
        {
            if (_reviewRepo.GetById(reviewId) == null)
            {
                throw new ReviewNotFoundException("\nReview Not Found.");
            }
        }
    }
}
