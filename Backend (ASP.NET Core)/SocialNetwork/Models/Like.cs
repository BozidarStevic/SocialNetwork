using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace SocialNetwork.Models
{
    public class Like
    {
        public int Id { get; set; }
        public string? UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
        public int PostId { get; set; }

        [JsonIgnore]
        public Post? Post { get; set; }
    }
}
