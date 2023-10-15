using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DTOs
{
    public class CommentPostRequestDTO
    {
        [Required]
        [MinLength(1)]
        public string? Text { get; set; }
    }
}
