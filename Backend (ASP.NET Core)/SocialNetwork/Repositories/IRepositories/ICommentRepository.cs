using SocialNetwork.Models;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentByIdAsync(int commentId);
        Task<Comment> AddCommentAsync(Comment comment);
    }
}
