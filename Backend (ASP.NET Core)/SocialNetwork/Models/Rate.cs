using System.Text.Json.Serialization;

namespace SocialNetwork.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public int RateValue { get; set; }

        public string? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public int? PostId { get; set; }
        [JsonIgnore]
        public Post? Post { get; set; }

    }
}
