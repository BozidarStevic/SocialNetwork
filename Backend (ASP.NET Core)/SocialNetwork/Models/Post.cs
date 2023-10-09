using System.Text.Json.Serialization;

namespace SocialNetwork.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int LikesCount { get; set; } = 0;
        public DateTime TimePosted { get; set; }
        public double AverageRate { get; set; } = 0;
        public int ViewsCount { get; set; } = 0;
        public string? Location { get; set; }

        public string? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }


        [JsonIgnore]
        public ICollection<Like>? Likes { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; }
        [JsonIgnore]
        public ICollection<Attachment>? Attachments { get; set; }
        [JsonIgnore]
        public ICollection<Label>? Labels { get; set; }
        [JsonIgnore]
        public ICollection<Rate>? Rates { get; set; }
        [JsonIgnore]
        public ICollection<View>? Views { get; set; }

    }
}
