using System;
using System.Threading.Tasks;
using Brain.Api.Models;
using Brain.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Brain.Api.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesRepository _categories;

        public CategoriesController(CategoriesRepository categoriesRepository) 
        {
            _categories = categoriesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var categories = await _categories.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            try
            {
                var category = await _categories.ShowAsync(id);
                return Ok(category);
            } catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Category category)
        {
            var response = await _categories.Create(category);
            return Ok(response);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var platform = await _categories.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]Category category)
        {
            try
            {
                var updatedPlatform = await _categories.UpdateAsync(category);
                return Ok(updatedPlatform);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}
