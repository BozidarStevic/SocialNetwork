using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DTOs
{
    public class PostRequestDTO
    {
        [Required]
        public string? Text { get; set; }

    }
}
