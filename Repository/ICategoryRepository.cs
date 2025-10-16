using TransactionWebAPI.Models;

namespace TransactionWebAPI.Repository
{
    public interface ICategoryRepository
    {
         Task<IEnumerable<Category>> GetCategoriesAsync();
         Task <Category> GetCategoryAsync(int id);    
         Task<Category> CreateCategoryAsync(Category category);
         Task<Category> UpdateCategoryAsync(Category category);
         Task DeleteCategoryAsync(int id);
    }
}
