
using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures;

namespace HW12_Issue1.Interfaces.Repository_Contracts
{
    public interface IUserRepository
    {
        public User? GetByUsername(string username);
        public User? GetById(int id);
        public void Add(User user);
        public void Update(User user);
        public void Delete(int id);

    }
}
