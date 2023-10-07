using Microsoft.AspNetCore.Identity;
using SocialNetwork.Models;

namespace SocialNetwork.DTOs
{
    public class UserResponseDTO
    {
        //public string? Id { get; set; }
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        //public string? Password { get; set; }
        //public ICollection<Post>? Posts { get; set; }
        //public ICollection<User>? Following { get; set; }
        //public ICollection<User>? Followers { get; set; }
        //public ICollection<IdentityRole>? Roles { get; set; }

        //public ICollection<Like>? Likes { get; set; }
        //public ICollection<Comment>? Comments { get; set; }

    }
}
