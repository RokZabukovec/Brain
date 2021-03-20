using System;
using System.Threading.Tasks;
using Brain.Api.Models;
using Brain.Api.Models.Account;
using Brain.Api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Brain.Api.Controllers
{
    [ApiController]
    [Route("/api/platforms")]
    public class PlatformsController : ControllerBase
    {
        private readonly PlatformsRepository _platforms;
        
        private readonly UserManager<User> _userManager;

        public PlatformsController(PlatformsRepository platformsRepository, UserManager<User> userManager) 
        {
            _platforms = platformsRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userTest = await _userManager.FindByIdAsync("a5ec1c41-b9a9-4078-8ee7-b58bbbc68759");
                var platforms = await _platforms.GetAll(userTest);
                return Ok(platforms);
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
                var platform = await _platforms.ShowAsync(id);
                return Ok(platform);
            } catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Platform platform)
        {
            var userTest = await _userManager.FindByIdAsync("a5ec1c41-b9a9-4078-8ee7-b58bbbc68759");
            platform.User = userTest;
            var response = await _platforms.Create(platform);
            return Ok(response);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var platform = await _platforms.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]Platform platform)
        {
            try
            {
                var updatedPlatform = await _platforms.UpdateAsync(platform);
                return Ok(updatedPlatform);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}
