using System.Text.Json.Serialization;

namespace SocialNetwork.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime TimePosted { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }
        public int PostId { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }
    }
}
