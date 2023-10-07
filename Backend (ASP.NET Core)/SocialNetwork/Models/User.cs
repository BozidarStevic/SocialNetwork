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
        public ICollection<FollowingUser>? Following { get; set; }
        [JsonIgnore]
        public ICollection<FollowingUser>? Followers { get; set; }
        
        public ICollection<IdentityRole>? Roles { get; set; }
        [JsonIgnore]
        public ICollection<Post>? Posts { get; set; }
        [JsonIgnore]
        public ICollection<Like>? Likes { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; }
        [JsonIgnore]
        public ICollection<Rate>? Rates { get; set; }
        [JsonIgnore]
        public ICollection<View>? Views { get; set; }
    }
}
