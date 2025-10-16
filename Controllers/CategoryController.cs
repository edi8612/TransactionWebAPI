using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Xml.Linq;
using TransactionWebAPI.Models;
using TransactionWebAPI.Models.Dto;
using TransactionWebAPI.Repository;
using TransactionWebAPI.Services;

namespace TransactionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }


        [HttpGet(Name ="GetCategories")]

        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);



        }

        [HttpGet("{id}",Name ="GetCategory")]
        public async Task <ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetAsync(id);
            if ( category == null)
            {
                return NotFound();
            }


            return Ok(category);


        }




        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryCreateDTO dto)
        {
            try
            {


                var createdCategory = await _categoryService.CreateAsync(dto);
                return CreatedAtRoute(
                    "GetCategory",
                    new { id = createdCategory.Id },
                    createdCategory);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, CategoryUpdateDTO dto)
        {
            try
            {
                dto.Id = id; // Ensure the DTO has the correct ID from the route
                var categoryUpdated = await _categoryService.UpdateAsync(dto);

                if (categoryUpdated == null)
                {
                    return NotFound();
                }

                return Ok(categoryUpdated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


    }
}
