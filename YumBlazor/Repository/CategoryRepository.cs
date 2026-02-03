using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) { _db = db; }

        public async Task<Category> CreateAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) return false;

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Category> GetAsync(int id)
        {
            return await _db.Categories.FindAsync(id) ?? throw new InvalidOperationException($"Category with id {id} not found.");
        }
        

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync(); 
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var existingCategory = await _db.Categories.FindAsync(category.Id);
            if (existingCategory == null)
            {
                throw new InvalidOperationException($"Category with id {category.Id} not found.");
            }

            existingCategory.Name = category.Name;

            await _db.SaveChangesAsync();
            return existingCategory;
        }
    }
}
