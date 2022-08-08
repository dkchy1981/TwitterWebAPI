using System.ComponentModel.DataAnnotations;

namespace TwitterDemoAPI.Models
{
    public class TweetResponse
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "TweetId is required")]
        public int TweetId { get; set; }

        [Required(ErrorMessage = "User is is required")]
        public string UserId { get; set; }

        public bool Like { get; set; }

        [MaxLength(200, ErrorMessage = "Max 200 characers")]
        public string Comments { get; set; }
    }
}
