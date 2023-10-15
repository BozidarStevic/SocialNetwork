using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Services.IServices
{
    public interface ICommentService
    {
        public Task<CommentResponseDTO> GetCommentByIdAsync(int id);
        public Task<CommentResponseDTO> AddCommentAsync(string userId, int postId, string text);
    }
}
