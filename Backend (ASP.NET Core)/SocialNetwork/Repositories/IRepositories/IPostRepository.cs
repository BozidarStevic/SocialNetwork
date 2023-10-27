using SocialNetwork.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface IPostRepository
    {
        Task<Post> GetPostByIdAsync(int postId);
        Task<Post> CreatePostAsync(Post post);
        Task<List<Post>> GetAllPostsSortedByDateTimeAsync();
        Task<Post> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(Post post);
    }
}
