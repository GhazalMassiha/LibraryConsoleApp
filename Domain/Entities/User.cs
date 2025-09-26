

using HW12_Issue1.Enums;

namespace HW12_Issue1.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public List<BorrowedBook>? BorrowedBooks { get; set; }
        public List<Review>? Reviews { get; set; }

        public User()
        {
            Role = RoleEnum.User;
        }
    }
}
