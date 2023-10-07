using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Services.IServices
{
    public interface IUserService
    {
        Task<bool> FollowUserAsync(string followerId, string followedId);
        Task<bool> UnfollowUserAsync(string followerId, string unfollowedId);
    }
}
