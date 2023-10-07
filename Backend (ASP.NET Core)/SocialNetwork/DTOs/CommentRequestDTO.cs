using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DTOs
{
    public class CommentRequestDTO
    {
        [Required]
        public string? Text { get; set; }

    }
}
