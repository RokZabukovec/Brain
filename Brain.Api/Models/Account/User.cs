using System;
using Microsoft.AspNetCore.Identity;

namespace Brain.Api.Models.Account
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
    }
}