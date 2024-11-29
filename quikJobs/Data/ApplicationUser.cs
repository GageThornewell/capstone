using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace quikJobs.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string? FirstName { get; set; } = "";

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string? LastName { get; set; } = "";

        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string? Address { get; set; } = "";
    }


    
}
