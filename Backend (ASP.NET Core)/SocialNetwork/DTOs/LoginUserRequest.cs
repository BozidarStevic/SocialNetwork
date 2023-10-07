using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DTOs
{
    public class LoginUserRequest
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
