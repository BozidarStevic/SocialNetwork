using SocialNetwork.DTOs;

namespace SocialNetwork.Services.IServices
{
    public interface ILabelService
    {
        public Task<LabelResponseDTO> GetLabelByIdAsync(int id);
        public Task<LabelResponseDTO> createLabelAsync(LabelRequestDTO labelRequestDTO);
    }
}
