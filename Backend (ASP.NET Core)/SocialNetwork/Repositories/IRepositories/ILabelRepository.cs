using SocialNetwork.Models;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface ILabelRepository
    {
        public Task<Label> GetLabelByIdAsync(int labelId);
        public Task<Label> GetLabelByNameAsync(string labelName);
        public Task<Label> CreateLabelAsync(Label label);
    }
}
