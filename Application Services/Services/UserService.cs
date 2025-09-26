

using ConsoleTables;
using HW12_Issue1.Application_Services.ServiceExceptions;
using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures.Repositories;
using HW12_Issue1.Interfaces.Service_Contracts;

namespace HW12_Issue1.Application_Services.Services
{
    public class UserService : IUserService
    {
        private CategoryRepository _categoryRepo = new CategoryRepository();
        private BookRepository _bookRepo = new BookRepository();
        private BorrowedBookRepository _borrowRepo = new BorrowedBookRepository();
        private UserRepository _userRepo = new UserRepository();
        private ReviewRepository _reviewRepo = new ReviewRepository();


        public void ViewMyBorrowedBooks(User user)
        {
            var borrowed = _borrowRepo.GetByUserId(user.Id);
            if (borrowed == null || borrowed.Count == 0)
            {
                Console.WriteLine("\nYou Have Not Borrowed Any Books Yet.\n");
                return;
            }

            var table = new ConsoleTable("BorrowedID", "BookID", "BorrowedTime");
            foreach (var bb in borrowed)
            {
                table.AddRow(bb.Id, bb.BookId, bb.BorrowedTime.ToString("g"));
            }
            table.Write();
            Console.WriteLine();
        }

        public bool BorrowBook(User user, int bookId)
        {
            var book = _bookRepo.GetById(bookId);

            if (book == null || book.IsBorrowed) 
                return false;


            book.IsBorrowed = true;
            _bookRepo.Update(book);


            BorrowedBook borrowedBook = new BorrowedBook
            {
                UserId = user.Id,
                BookId = bookId,
                BorrowedTime = DateTime.Now
            };
            _borrowRepo.Add(borrowedBook);


            if (user.BorrowedBooks == null)
                user.BorrowedBooks = new List<BorrowedBook>();

            user.BorrowedBooks.Add(borrowedBook);
            _userRepo.Update(user);

            return true;
        }


        public bool ReturnBook(User user, int borrowedId)
        {
            var borrowedList = _borrowRepo.GetByUserId(user.Id);
            var borrowedBook = borrowedList?.Find(b => b.Id == borrowedId);
            if (borrowedBook == null)
            {
                return false;
            }

            var book = _bookRepo.GetById(borrowedBook.BookId);
            if (book != null)
            {
                book.IsBorrowed = false;
                _bookRepo.Update(book);
            }

            _borrowRepo.Delete(borrowedId);

            if (user.BorrowedBooks != null)
            {
                var bookToRemove = user.BorrowedBooks.Find(b => b.Id == borrowedId);
                if (bookToRemove != null)
                    user.BorrowedBooks.Remove(bookToRemove);
            }
            _userRepo.Update(user);

            return true;
        }

        public void ViewMyReviews(User user)
        {
            var reviews = _reviewRepo.GetByUserId(user.Id);
            if (reviews == null || reviews.Count == 0)
            {
                Console.WriteLine("\nYou Have Left No Comments.\n");
                return;
            }

            var table = new ConsoleTable("ReviewID", "BookID", "Rating", "Comment", "CreatedAt");
            foreach (var r in reviews)
            {
                table.AddRow(r.Id, r.BookId, r.Rating, r.Comment ?? "", r.CreatedAt.ToString("g"));
            }
            table.Write();
            Console.WriteLine();
        }

        public void AddReview(User user, int bookId, string? comment, int rating)
        {
            var review = new Review
            {
                UserId = user.Id,
                BookId = bookId,
                Comment = comment,
                Rating = rating,
                CreatedAt = DateTime.Now,
            };
            _reviewRepo.Add(review);
        }

        public void EditReview(User user, int reviewId, string? newComment, int newRating)
        {
            var review = _reviewRepo.GetById(reviewId);

            if (review == null || review.UserId != user.Id)
                throw new ReviewNotFoundException("\nReview Not Found.");

            review.Comment = newComment;
            review.Rating = newRating;
            review.CreatedAt = DateTime.Now;
            _reviewRepo.Update(review);
        }

        public void DeleteReview(User user, int reviewId)
        {
            var review = _reviewRepo.GetById(reviewId);
            if (review == null || review.UserId != user.Id)
                throw new ReviewNotFoundException("\nReview Not Found.");

            _reviewRepo.Delete(reviewId);
        }
    }
}
