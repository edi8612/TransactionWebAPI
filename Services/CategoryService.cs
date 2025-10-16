using AutoMapper;
using TransactionWebAPI.Models;
using TransactionWebAPI.Models.Dto;
using TransactionWebAPI.Repository;

namespace TransactionWebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        private readonly IMapper _mapper;


        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateAsync(CategoryCreateDTO dto)
        {
            try
            {

                var category = _mapper.Map<Category>(dto);

                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    throw new ArgumentException("Category name cannot be empty.");
                }

                await _categoryRepo.CreateCategoryAsync(category);

                return _mapper.Map<CategoryDTO>(category);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error creating category");
            }



        }

        public async Task DeleteAsync(int id)
        {
            try
            {

                await _categoryRepo.DeleteCategoryAsync(id);

                if (id <= 0)
                {
                    throw new ArgumentException("Invalid category ID.");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error deleting category");
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            try
            {
                var categories = await _categoryRepo.GetCategoriesAsync();
                if (categories == null || !categories.Any())
                {
                    throw new ApplicationException("No categories found.");
                }
                return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while retrieving categories.", ex);
            }

        }

        public async Task<CategoryDTO> GetAsync(int id)
        {
            try
            {
                var category = await _categoryRepo.GetCategoryAsync(id);
                if (category == null)
                {
                    throw new KeyNotFoundException($"Category with ID {id} not found.");
                }
                return _mapper.Map<CategoryDTO>(category);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error fetching category");
            }

        }

        public async Task<CategoryDTO> UpdateAsync(CategoryUpdateDTO dto)
        {
            try
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
            catch (Exception ex)
            {
                throw new ArgumentException("Error updating category");
            }

        }
    }
}
