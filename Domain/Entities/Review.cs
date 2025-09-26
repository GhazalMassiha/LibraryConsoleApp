﻿

namespace HW12_Issue1.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt {  get; set; }
        public User User { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
