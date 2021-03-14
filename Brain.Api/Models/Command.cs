using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Brain.Api.Models
{
    public class Command
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The command is required.")]
        [MaxLength(255, ErrorMessage = "The command should be less than 255 characters.")]
        [MinLength(2, ErrorMessage = "The command should be at least 2 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description is required.")]
        [MaxLength(255, ErrorMessage = "The description should be less than 255 characters.")]
        [MinLength(2, ErrorMessage = "The description should be at least 2 characters long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The category is required.")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Command()
        {
            var date = DateTime.UtcNow;
            this.CreatedDate = date;
            this.ModifiedDate = date;
        }
    }
}
