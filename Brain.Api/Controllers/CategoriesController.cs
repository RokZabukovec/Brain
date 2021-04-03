using System;
using System.Security.Claims;
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
    [Authorize(AuthenticationSchemes = 
        JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesRepository _categories;

        private readonly UserManager<User> _userManager;
        
        public CategoriesController(CategoriesRepository categoriesRepository, CommandsRepository commands,  UserManager<User> userManager) 
        {
            _categories = categoriesRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var jwtUser = HttpContext.User;
                var userId= jwtUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    var categories = await _categories.GetAll(user);
                    return Ok(categories);
                }
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
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
