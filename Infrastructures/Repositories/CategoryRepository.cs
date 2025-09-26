

using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Infrastructures.LContext;
using HW12_Issue1.Interfaces.Repository_Contracts;

namespace HW12_Issue1.Infrastructures.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private LibraryContext _Lc = new LibraryContext();

        public List<Category> GetAll()
        {
            return _Lc.Categories.ToList();
        }

        public Category? GetById(int id)
        {
            return _Lc.Categories.SingleOrDefault(x => x.Id == id);
        }

        public Category? GetByName(string name)
        {
            return _Lc.Categories.FirstOrDefault(x => x.Name == name);
        }

        public void Add(Category category)
        {
            _Lc.Categories.Add(category);
            _Lc.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _Lc.Categories.SingleOrDefault(x => x.Id == id);
            if (category != null)
            {
                _Lc.Categories.Remove(category);
                _Lc.SaveChanges();
            }
        }
    }
}
