using SocialNetwork.Models;

namespace SocialNetwork.DTOs
{
    public class UserRequestDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<User> Following { get; set; } = new List<User>();
        public List<User> Followers { get; set; } = new List<User>();
    }
}
