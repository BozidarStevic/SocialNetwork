using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Repositories;
using SocialNetwork.Repositories.IRepositories;
using SocialNetwork.Services.IServices;

namespace SocialNetwork.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> FollowUserAsync(string followerId, string followedId)
        {
            var isFollowing = await IsFollowingUserAsync(followerId, followedId);

            if (!isFollowing)
            {
                var followingUser = new FollowingUser
                {
                    FollowerId = followerId,
                    FollowedUserId = followedId
                };

                await _userRepository.FollowUserAsync(followingUser);
                return true;
            }

            return false; // Korisnik već prati drugog korisnika
        }

        public async Task<bool> IsFollowingUserAsync(string followerId, string followedId)
        {
            return await _userRepository.IsFollowingUserAsync(followerId, followedId);
        }

        public async Task<bool> UnfollowUserAsync(string followerId, string unfollowedId)
        {
            var isFollowing = await IsFollowingUserAsync(followerId, unfollowedId);

            if (isFollowing)
            {
                var followingUser = new FollowingUser
                {
                    FollowerId = followerId,
                    FollowedUserId = unfollowedId
                };

                await _userRepository.UnfollowUserAsync(followingUser);
                return true;
            }

            return false; // Korisnik ne prati drugog korisnika
        }

    }
}
