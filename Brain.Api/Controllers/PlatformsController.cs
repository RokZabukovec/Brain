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
                var jwtUser = HttpContext.User;
                var userId= jwtUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    var platforms = await _platforms.GetAll(user);
                    return Ok(platforms); 
                }
                return Unauthorized();
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
            try
            {
                var user = HttpContext.User;
                var userId= user.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    var userTest = await _userManager.FindByIdAsync(userId);
                    platform.User = userTest;
                    var response = await _platforms.Create(platform);
                    return Ok(response);
                }

                return Unauthorized();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var user = HttpContext.User;
                var userId= user.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    var platform = await _platforms.ShowAsync(id);

                    if (platform == null)
                    {
                        return NotFound();
                    }
                    
                    if (platform.UserId.ToString().ToLower() == userId.ToLower())
                    {
                        await _platforms.DeleteAsync(id);
                        return Ok(platform);
                    }

                    return Unauthorized();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]Platform platform)
        {
            try
            {
                var user = HttpContext.User;
                var userId= user.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    if (platform.UserId.ToString().ToLower() == userId.ToLower())
                    {
                        var updatedPlatform = await _platforms.UpdateAsync(platform);
                        return Ok(updatedPlatform);
                    }

                    return Unauthorized();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return BadRequest();
        }
    }
}
