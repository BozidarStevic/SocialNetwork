using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialNetwork.Data;
using SocialNetwork.Models;
using SocialNetwork.Repositories.IRepositories;

namespace SocialNetwork.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _collection;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _collection = _context.Users;
        }

        public async Task FollowUserAsync(FollowingUser followingUser)
        {
            _context.FollowingUsers.Add(followingUser);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsFollowingUserAsync(string followerId, string followedId)
        {
            return await _context.FollowingUsers
                .AnyAsync(fu => fu.FollowerId == followerId && fu.FollowedUserId == followedId);
        }

        public async Task UnfollowUserAsync(FollowingUser followingUser)
        {
            _context.FollowingUsers.Remove(followingUser);
            await _context.SaveChangesAsync();
        }

    }
}
