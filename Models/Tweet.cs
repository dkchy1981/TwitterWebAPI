using System.ComponentModel.DataAnnotations;

namespace TwitterDemoAPI.Models
{
    public class Tweet
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Tag is required")]
        [MaxLength(50,ErrorMessage ="Max 50 characers")]
        public string Tag { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(200, ErrorMessage = "Max 200 characers")]
        public string Message { get; set; }
    }
}
