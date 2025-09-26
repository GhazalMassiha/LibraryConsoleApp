
using HW12_Issue1.Application_Services.ServiceExceptions;
using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures.Repositories;
using HW12_Issue1.Interfaces.Service_Contracts;
using ConsoleTables;

namespace HW12_Issue1.Application_Services.Services
{
    public class BookService : IBookService
    {
        private CategoryRepository _categoryRepo = new CategoryRepository();
        private BookRepository _bookRepo = new BookRepository();
        private ReviewRepository _reviewRepo = new ReviewRepository();

        public void ViewAllBooks()
        {
            var books = _bookRepo.GetAll();
            if (books == null || books.Count == 0)
            {
                Console.WriteLine("\nNo Books Found.\n");
                return;
            }

            var table = new ConsoleTable("ID", "Title", "CategoryId", "IsBorrowed", "Rating");

            foreach (var b in books)
            {
                double avg = GetAverageRating(b.Id);              //format as fixed-point with 2 decimals
                string avgRating = avg == 0.0 ? "No Reviews Yet" : avg.ToString("F2");

                table.AddRow(b.Id, b.Title, b.CategoryId, b.IsBorrowed, avgRating);
            }

            table.Write();
            Console.WriteLine();
        }


        public void ViewAllCategories()
        {
            var categories = _categoryRepo.GetAll();
            if (categories == null || categories.Count == 0)
            {
                Console.WriteLine("\nNo Categories Found.\n");
                return;
            }

            var table = new ConsoleTable("ID", "Name");
            foreach (var c in categories)
            {
                table.AddRow(c.Id, c.Name);
            }
            table.Write();
            Console.WriteLine();
        }

        // --- Helper Methods ---

        private double GetAverageRating(int bookId)
        {
            var review = _reviewRepo.GetByBookId(bookId);

            if (review == null || review.Count == 0)
                return 0;

            double sum = review.Sum(r => r.Rating);
            double avg = sum / review.Count;

            return avg;
        }
    }
}
