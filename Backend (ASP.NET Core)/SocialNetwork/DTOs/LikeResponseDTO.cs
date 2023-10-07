using SocialNetwork.Models;

namespace SocialNetwork.DTOs
{
    public class LikeResponseDTO
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public Post? Post { get; set; }
    }
}
