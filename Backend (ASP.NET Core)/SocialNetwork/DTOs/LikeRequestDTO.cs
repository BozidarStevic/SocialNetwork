using SocialNetwork.Models;

namespace SocialNetwork.DTOs
{
    public class LikeRequestDTO
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public Post? Post { get; set; }
    }
}
