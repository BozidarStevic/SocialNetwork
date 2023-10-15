using SocialNetwork.DTOs;
using SocialNetwork.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SocialNetwork.Services.IServices
{
    public interface IPostService
    {
        public Task<PostResponseDTO> GetPostByIdAsync(int id);
        public Task<PostResponseDTO> CreatePostAsync(PostRequestDTO post, User user);
    }
}
