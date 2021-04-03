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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/commands")]
    public class CommandsController : ControllerBase
    {
        private readonly CommandsRepository _commands;
        
        private readonly UserManager<User> _userManager;

        public CommandsController(CommandsRepository commandsRepository, UserManager<User> userManager) 
        {
            _commands = commandsRepository;
            
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jwtUser = HttpContext.User;
            var userId= jwtUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var commands = await _commands.GetAll(user);
                return Ok(commands);
            }
            return Unauthorized();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            try
            {
                var command = await _commands.ShowAsync(id);
                return Ok(command);
            } catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Command command)
        {
            var response = await _commands.Create(command);
            return Ok(response);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var jwtUser = HttpContext.User;
                var userId = jwtUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (userId != null)
                {
                    await _commands.DeleteAsync(id);
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return NotFound();
            }

            return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]Command command)
        {
            try
            {
                var updatedCommand = await _commands.UpdateAsync(command);
                return Ok(updatedCommand);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

    }
}
