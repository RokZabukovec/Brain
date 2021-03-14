using System;
using Brain.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Brain.Api.Data
{
    public class BrainDbContext : DbContext
    {
        public DbSet<Platform> Platforms { get; set; }

        public DbSet<Command> Commands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public BrainDbContext(DbContextOptions<BrainDbContext> options) : base(options) { }
    }
}
