
namespace SocialNetwork.DTOs
{
    public class PostResponseDTO
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int LikesCount { get; set; }
        public DateTime TimePosted { get; set; }
        public double AverageRate { get; set; }
        public int ViewsCount { get; set; }
        public string? Location { get; set; }
        public UserResponseDTO? User { get; set; }
        public ICollection<LikeResponseDTO>? Likes { get; set; }
        public ICollection<CommentResponseDTO>? Comments { get; set; }
        public ICollection<AttachmentResponseDTO>? Attachments { get; set; }
        public ICollection<LabelResponseDTO>? Labels { get; set; }
        public ICollection<ViewResponseDTO>? Views { get; set; }
    }
}
