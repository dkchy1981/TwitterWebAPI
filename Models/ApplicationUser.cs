using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoreWebAPIDapperPractice1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50,ErrorMessage ="Max 50 characers")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characers")]
        public string LastName { get; set; }
    }
}
