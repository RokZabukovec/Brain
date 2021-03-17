using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brain.Api.Models;
using Brain.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Brain.Api.Controllers
{
    [ApiController]
    [Route("/api/platforms")]
    public class PlatformsController : ControllerBase
    {
        private readonly PlatformsRepository _platforms;

        public PlatformsController(PlatformsRepository platformsRepository) 
        {
            _platforms = platformsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var platforms = await _platforms.GetAll();
            return Ok(platforms);
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
