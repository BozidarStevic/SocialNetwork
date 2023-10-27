using SocialNetwork.DTOs;
using SocialNetwork.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SocialNetwork.Services.IServices
{
    public interface IPostService
    {
        Task<PostResponseDTO> GetPostByIdAsync(int id);
        Task<PostResponseDTO> CreatePostAsync(PostRequestDTO post, User user);
        Task<List<PostResponseDTO>> GetAllPostDTOsSortedByDateTimeAsync(User user);
        Task<PostResponseDTO> UpdatePostAsync(string currentUserId, int postId, UpdatePostDTO updatePostDTO);
    }
}
