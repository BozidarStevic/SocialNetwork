using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Services.IServices
{
    public interface IRateService
    {
        Task<PostResponseDTO> RatePostAsync(User user, int postId, int rateValue);
    }
}
