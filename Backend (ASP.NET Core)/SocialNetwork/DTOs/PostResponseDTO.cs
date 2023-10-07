
namespace SocialNetwork.DTOs
{
    public class PostResponseDTO
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int LikesCount { get; set; }
        public DateTime TimePosted { get; set; }
        public UserResponseDTO? User { get; set; }
        public ICollection<LikeResponseDTO>? Likes { get; set; }
        public ICollection<CommentResponseDTO>? Comments { get; set; }
    }
}
