using TransactionWebAPI.Models.Dto;

namespace TransactionWebAPI.Services
{
    public interface ICategoryService
    {


        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetAsync(int id);
        Task<CategoryDTO> CreateAsync(CategoryCreateDTO dto);
        Task<CategoryDTO> UpdateAsync(CategoryUpdateDTO dto);
        Task DeleteAsync(int id);




    }
}
