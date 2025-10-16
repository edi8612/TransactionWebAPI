using AutoMapper;
using TransactionWebAPI.Models;
using TransactionWebAPI.Models.Dto;
using TransactionWebAPI.Repository;

namespace TransactionWebAPI.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
       
        private readonly IMapper _mapper;


        public CategoryService( ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateAsync(CategoryCreateDTO dto)
        {
            var category = _mapper.Map<Category>(dto);

            await _categoryRepo.CreateCategoryAsync(category);

            return _mapper.Map<CategoryDTO>(category);

        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepo.DeleteCategoryAsync(id);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepo.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories); 
        }

        public async Task<CategoryDTO> GetAsync(int id)
        {
            var category = await _categoryRepo.GetCategoryAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> UpdateAsync(CategoryUpdateDTO dto)
        {
            var category = await _categoryRepo.GetCategoryAsync(dto.Id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {dto.Id} not found.");
            }

            _mapper.Map(dto, category); 
            await _categoryRepo.UpdateCategoryAsync(category);

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
