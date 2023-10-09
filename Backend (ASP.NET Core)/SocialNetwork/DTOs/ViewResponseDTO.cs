using SocialNetwork.Models;

namespace SocialNetwork.DTOs
{
    public class ViewResponseDTO
    {
        public int Id { get; set; }
        
        public UserResponseDTO? User { get; set; }

        public int? PostId { get; set; }
    }
}
