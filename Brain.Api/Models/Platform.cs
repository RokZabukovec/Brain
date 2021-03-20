using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Brain.Api.Models.Account;

namespace Brain.Api.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [MaxLength(255, ErrorMessage = "The name should be less than 255 characters.")]
        [MinLength(2, ErrorMessage = "The name should be at least 2 characters long.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<Category> Categories { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage = "The user is required.")]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Platform()
        {
            var date = DateTime.UtcNow;
            CreatedDate = date;
            ModifiedDate = date;
        }

    }
}