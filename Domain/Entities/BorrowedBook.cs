

namespace HW12_Issue1.Domain.Entities
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowedTime { get; set; }

        // --- Navigation Properties ---
        public User User { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
