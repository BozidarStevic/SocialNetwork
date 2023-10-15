using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialNetwork.Data;
using SocialNetwork.Models;
using SocialNetwork.Repositories.IRepositories;
using static System.Net.Mime.MediaTypeNames;

namespace SocialNetwork.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Post> _collection;

        public PostRepository(AppDbContext context)
        {
            _context = context;
            _collection = _context.Posts;
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await _context.Posts
                .Where(post => post.Id == postId)
                .Include(post => post.User)
                .Include(post => post.Likes)
                .Include(post => post.Attachments)
                .Include(post => post.Labels)
                .Include(post => post.Rates)
                .Include(post => post.Views)
                .FirstOrDefaultAsync();
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            await _collection.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }


    }
}
