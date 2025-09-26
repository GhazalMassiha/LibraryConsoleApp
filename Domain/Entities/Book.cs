

namespace HW12_Issue1.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public bool IsBorrowed { get; set; }
        public List<Review>? Reviews { get; set; }

        // --- Navigation Properties ---
        public Category Category { get; set; } = null!;
        public List<BorrowedBook>? BorrowedBooks { get; set; }
    }
}
