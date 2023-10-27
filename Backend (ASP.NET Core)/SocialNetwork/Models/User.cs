using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialNetwork.Models
{
    [Table("AspNetUsers")]
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [JsonIgnore]
        public ICollection<FollowingUser>? Following { get; set; } = new List<FollowingUser>();
        [JsonIgnore]
        public ICollection<FollowingUser>? Followers { get; set; } = new List<FollowingUser>();

        public ICollection<IdentityRole>? Roles { get; set; } = new List<IdentityRole>();
        [JsonIgnore]
        public ICollection<Post>? Posts { get; set; } = new List<Post>();
        [JsonIgnore]
        public ICollection<Like>? Likes { get; set; } = new List<Like>();
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        [JsonIgnore]
        public ICollection<Rate>? Rates { get; set; } = new List<Rate>();
        [JsonIgnore]
        public ICollection<View>? Views { get; set; } = new List<View>();
    }
}
