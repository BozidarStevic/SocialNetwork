using System.Text.Json.Serialization;

namespace SocialNetwork.Models
{
    public class Label
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<Post>? Posts { get; set; } = new List<Post>();

    }
}
