using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Services.IServices
{
    public interface ICommentService
    {
        Task<CommentResponseDTO> GetCommentByIdAsync(int id);
        Task<CommentResponseDTO> AddCommentAsync(string userId, int postId, string text);
        Task UpdateCommentTextAsync(string currentUserId, int commentId, string commentText);
        Task<bool> DeleteCommentAsync(int commentId, string userId);
    }
}
