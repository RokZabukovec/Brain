using System;
using Brain.Api.Models;
using Brain.Api.Models.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Brain.Api.Data
{
    public class BrainDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Platform> Platforms { get; set; }

        public DbSet<Command> Commands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public BrainDbContext(DbContextOptions<BrainDbContext> options) : base(options) { }
    }
}
