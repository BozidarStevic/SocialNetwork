using SocialNetwork.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface IPostRepository
    {
        Task<Post> GetPostByIdAsync(int postId);
        public Task<Post> CreatePost(Post post);
    }
}
