namespace SocialNetwork.DTOs
{
    public class AttachmentResponseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Type { get; set; } // "image", "video", "url" ...
        public int PostId { get; set; }
    }
}
