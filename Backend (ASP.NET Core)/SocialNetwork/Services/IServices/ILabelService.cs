using SocialNetwork.DTOs;

namespace SocialNetwork.Services.IServices
{
    public interface ILabelService
    {
        Task<LabelResponseDTO> GetLabelByIdAsync(int id);
        Task<LabelResponseDTO> createLabelAsync(LabelRequestDTO labelRequestDTO);
    }
}
