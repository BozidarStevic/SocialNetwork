using SocialNetwork.Models;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface IViewRepository
    {
        Task CreateViewAsync(Post post, User user);
        Task<View> findViewByUserAndPostAsync(Post post, User user);
    }
}
