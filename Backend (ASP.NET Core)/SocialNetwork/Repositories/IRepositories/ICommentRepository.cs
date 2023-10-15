using SocialNetwork.Models;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        public Task<Comment> GetCommentByIdAsync(int commentId);
        public Task<Comment> AddCommentAsync(Comment comment);
    }
}
