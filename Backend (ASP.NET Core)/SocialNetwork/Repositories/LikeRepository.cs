using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialNetwork.Data;
using SocialNetwork.Models;
using SocialNetwork.Repositories.IRepositories;

namespace SocialNetwork.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Like> _collection;

        public LikeRepository(AppDbContext context)
        {
            _context = context;
            _collection = _context.Likes;
        }

        
        
    }
}
