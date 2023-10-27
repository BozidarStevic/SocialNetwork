using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Services.IServices
{
    public interface ICommentService
    {
        Task<CommentResponseDTO> GetCommentByIdAsync(int id);
        Task<CommentResponseDTO> AddCommentAsync(string userId, int postId, string text);
    }
}
