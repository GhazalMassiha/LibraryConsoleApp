

using HW12_Issue1.Domain.Entities;

namespace HW12_Issue1.Interfaces.Repository_Contracts
{
    public interface ICategoryRepository
    {
        public List<Category> GetAll();
        public Category? GetById(int id);
        public Category? GetByName(string name);
        public void Add(Category category);
        public void Delete(int id);
    }
}
