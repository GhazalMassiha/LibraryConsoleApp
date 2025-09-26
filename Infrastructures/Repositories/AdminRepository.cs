using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures.LContext;
using HW12_Issue1.Interfaces.Repository_Contracts;
using HW12_Issue1.Interfaces.Service_Contracts;

namespace HW12_Issue1.Infrastructures.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private LibraryContext _Lc = new LibraryContext();

        public Admin? GetByUsername(string username)
        {
            return _Lc.Admins.SingleOrDefault(a => a.Username == username);
        }

        public void Add(Admin admin)
        {
            _Lc.Admins.Add(admin);
            _Lc.SaveChanges();
        }

        public void Delete(int id)
        {
            var admin = _Lc.Admins.SingleOrDefault(x => x.Id == id);
            if (admin != null)
            {
                _Lc.Admins.Remove(admin);
                _Lc.SaveChanges();
            }
        }
    }
}