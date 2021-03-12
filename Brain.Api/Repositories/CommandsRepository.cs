using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brain.Api.Data;
using Brain.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Brain.Api.Repositories
{
    public class CommandsRepository
    {
        protected readonly BrainDbContext _db;

        public CommandsRepository(BrainDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<List<Command>> GetAll()
        {
            return await _db.Commands.ToListAsync();
        }

        public async Task<Command> Create(Command command)
        {
            _db.Commands.Add(command);
            await _db.SaveChangesAsync();
            return command;
        }

        public async Task<Command> ShowAsync(int Id)
        {
            return await _db.Commands.FirstAsync(command => command.Id == Id);
        }

        public async Task<Command> DeleteAsync(int Id)
        {
            Command command = new Command() { Id = Id};
            _db.Commands.Attach(command);
            _db.Commands.Remove(command);
            await _db.SaveChangesAsync();
            return command;
        }

        public async Task<Command> UpdateAsync(Command newCommand)
        {
            var command = await ShowAsync(newCommand.Id);
            command.Name = newCommand.Name;
            command.Description = newCommand.Description;
            command.CategoryId = command.CategoryId;
            command.ModifiedDate = DateTime.UtcNow;
            _db.Commands.Update(command);
            await _db.SaveChangesAsync();
            return command;
        }
    }
}
