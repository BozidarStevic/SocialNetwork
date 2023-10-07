
namespace SocialNetwork.DTOs
{
    public class CommentResponseDTO
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public UserResponseDTO? User { get; set; }
        public DateTime TimePosted { get; set; }
    }
}
