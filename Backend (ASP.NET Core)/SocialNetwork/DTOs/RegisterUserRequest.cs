using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DTOs
{
    public class RegisterUserRequest
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
