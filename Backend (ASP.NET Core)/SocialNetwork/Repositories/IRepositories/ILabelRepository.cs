using SocialNetwork.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SocialNetwork.Repositories.IRepositories
{
    public interface ILabelRepository
    {
        Task<Label> GetLabelByIdAsync(int labelId);
        Task<Label> GetLabelByNameAsync(string labelName);
        Task<Label> CreateLabelAsync(Label label);
        Task<IEnumerable<Label>> GetAllLabelsAsync();
    }
}
