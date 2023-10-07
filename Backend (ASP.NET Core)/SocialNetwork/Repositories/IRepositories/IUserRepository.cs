using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task FollowUserAsync(FollowingUser followingUser);
        Task<bool> IsFollowingUserAsync(string followerId, string followedId);
        Task UnfollowUserAsync(FollowingUser followingUser);
    }
}
