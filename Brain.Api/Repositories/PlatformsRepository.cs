using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brain.Api.Data;
using Brain.Api.Models;
using Brain.Api.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace Brain.Api.Repositories
{
    public class PlatformsRepository
    {
        protected readonly BrainDbContext _db;

        public PlatformsRepository(BrainDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<List<Platform>> GetAll(User user)
        {
            return await _db.Platforms
                .Where(x => x.UserId == user.Id)
                .ToListAsync();
        }

        public async Task<Platform> Create(Platform platform)
        {
            _db.Platforms.Add(platform);
            await _db.SaveChangesAsync();
            return platform;
        }

        public async Task<Platform> ShowAsync(int Id)
        {
            return await _db.Platforms.FirstAsync(platform => platform.Id == Id);
        }

        public async Task<Platform> DeleteAsync(int Id)
        {
            Platform platform = new Platform() { Id = Id };
            _db.Platforms.Attach(platform);
            _db.Platforms.Remove(platform);
            await _db.SaveChangesAsync();
            return platform;
        }

        public async Task<Platform> UpdateAsync(Platform newPlatform)
        {
            var platform = await ShowAsync(newPlatform.Id);
            platform.Name = newPlatform.Name;
            platform.Description = newPlatform.Description;
            platform.ModifiedDate = DateTime.UtcNow;
            _db.Platforms.Update(platform);
            await _db.SaveChangesAsync();
            return platform;
        }
    }
}
