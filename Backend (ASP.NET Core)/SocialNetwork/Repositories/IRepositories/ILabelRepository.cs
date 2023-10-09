using SocialNetwork.Models;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface ILabelRepository
    {
        public Task<Label> GetLabelByIdAsync(int labelId);
    }
}
