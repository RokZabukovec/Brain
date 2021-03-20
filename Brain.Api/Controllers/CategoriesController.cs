using System;
using System.Threading.Tasks;
using Brain.Api.Models;
using Brain.Api.Models.Account;
using Brain.Api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Brain.Api.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesRepository _categories;
        private readonly UserManager<User> _userManager;
        public CategoriesController(CategoriesRepository categoriesRepository, UserManager<User> userManager) 
        {
            _categories = categoriesRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userTest = await _userManager.FindByIdAsync("a5ec1c41-b9a9-4078-8ee7-b58bbbc68759");
                var categories = await _categories.GetAll(userTest);
                return Ok(categories);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

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
            try
            {
                var response = await _categories.Create(category);
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _categories.DeleteAsync(id);
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

        [HttpGet("platform/{id:int}")]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            try
            {
                var categories = await _categories.GetByPlatformId(id);
                return Ok(categories);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
