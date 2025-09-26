

using HW12_Issue1.Domain.Entities;

namespace HW12_Issue1.Interfaces.Repository_Contracts
{
    public interface IAdminRepository
    {
        public Admin? GetByUsername(string username);
        public void Add(Admin admin);
        public void Delete(int id);
    }
}
