using SocialNetwork.Models;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface ILabelRepository
    {
        Task<Label> GetLabelByIdAsync(int labelId);
        Task<Label> GetLabelByNameAsync(string labelName);
        Task<Label> CreateLabelAsync(Label label);
    }
}
