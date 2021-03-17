using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brain.Api.Models;
using Brain.Api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brain.Api.Controllers
{
    [ApiController]
    [Route("/api/commands")]
    public class CommandsController : ControllerBase
    {
        private readonly CommandsRepository _commands;

        public CommandsController(CommandsRepository commandsRepository) 
        {
            _commands = commandsRepository;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var user = HttpContext.User;
           var commands = await _commands.GetAll();
            return Ok(commands);
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
                var command = await _commands.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }

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
