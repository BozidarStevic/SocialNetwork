using System.Text.Json.Serialization;

namespace SocialNetwork.Models
{
    public class FollowingUser
    {
        public int FollowingUserId { get; set; }

        public string? FollowerId { get; set; }
        [JsonIgnore]
        public User? Follower { get; set; }

        public string? FollowedUserId { get; set; }
        [JsonIgnore]
        public User? FollowedUser { get; set; }
    }
}
