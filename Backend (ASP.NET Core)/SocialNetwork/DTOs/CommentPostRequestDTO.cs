using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DTOs
{
    public class CommentPostRequestDTO
    {
        [Required]
        public string? Text { get; set; }
    }
}
