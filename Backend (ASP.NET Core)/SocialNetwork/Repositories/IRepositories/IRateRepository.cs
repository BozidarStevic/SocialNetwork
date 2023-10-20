using SocialNetwork.Models;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface IRateRepository
    {
        Task<Rate> CreateRateAsync(Rate rate);
        Task<List<Rate>> FindRatesByPostIdAsync(int postId);
    }
}
