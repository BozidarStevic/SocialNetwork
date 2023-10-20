using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Models;
using SocialNetwork.Repositories.IRepositories;

namespace SocialNetwork.Repositories
{
    public class ViewRepository : IViewRepository
    {
        private readonly AppDbContext _context;
        public ViewRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task CreateViewAsync(Post post, User user)
        {
            View view = new View() {
                Post = post,
                PostId = post.Id,
                User = user,
                UserId = user.Id,
            };
            await _context.Views.AddAsync(view);
            post.ViewsCount++;
            await _context.SaveChangesAsync();
        }

        public async Task<View> findViewByUserAndPostAsync(Post post, User user)
        {
            return await _context.Views.Where(v => v.UserId == user.Id && v.PostId == post.Id).FirstOrDefaultAsync();
        }
    }
}
