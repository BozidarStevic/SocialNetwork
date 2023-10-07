using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialNetwork.Data;
using SocialNetwork.Models;
using SocialNetwork.Repositories.IRepositories;

namespace SocialNetwork.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Comment> _collection;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
            _collection = _context.Comments;
        }

        

    }
}
