using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures.LContext;
using HW12_Issue1.Interfaces.Repository_Contracts;
using Microsoft.EntityFrameworkCore;

namespace HW12_Issue1.Infrastructures.Repositories
{
    public class UserRepository : IUserRepository
    {
        private LibraryContext _Lc = new LibraryContext();

        public User? GetByUsername(string username)
        {
            return _Lc.Users
                .Include(u => u.BorrowedBooks)
                .SingleOrDefault(u => u.Username == username);
        }

        public User? GetById(int id)
        {
            return _Lc.Users
                .Include(u => u.BorrowedBooks)
                .SingleOrDefault(u => u.Id == id);
        }

        public void Add(User user)
        {
            _Lc.Users.Add(user);
            _Lc.SaveChanges();
        }

        public void Update(User user)
        {
            _Lc.Users.Update(user);
            _Lc.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _Lc.Users.SingleOrDefault(x => x.Id == id);
            if (user != null)
            {
                _Lc.Users.Remove(user);
                _Lc.SaveChanges();
            }
        }
    }
}
