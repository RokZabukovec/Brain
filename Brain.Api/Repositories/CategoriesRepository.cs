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
    public class CategoriesRepository
    {
        protected readonly BrainDbContext _db;

        public CategoriesRepository(BrainDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<List<Category>> GetAll(User user)
        {
            return await _db.Categories
                .Where(x=> x.Platform.UserId == user.Id)
                .Include(y => y.Commands)
                .ToListAsync();
        }

        public async Task<Category> Create(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<Category> ShowAsync(int Id)
        {
            return await _db.Categories.FirstAsync(command => command.Id == Id);
        }

        public async Task<Category> DeleteAsync(int Id)
        {
            Category category = new Category() { Id = Id };
            _db.Categories.Attach(category);
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category newCategory)
        {
            var category = await ShowAsync(newCategory.Id);
            category.Name = newCategory.Name;
            category.Description = newCategory.Description;
            category.ModifiedDate = DateTime.UtcNow;
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetByPlatformId(int Id)
        {
           return await _db.Categories
               .Where(x => x.PlatformId == Id)
               .ToListAsync();
        }
    }
}
