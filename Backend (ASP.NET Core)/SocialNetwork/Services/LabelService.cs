using AutoMapper;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Repositories;
using SocialNetwork.Repositories.IRepositories;
using SocialNetwork.Services.IServices;
using static System.Net.Mime.MediaTypeNames;

namespace SocialNetwork.Services
{
    public class LabelService : ILabelService
    {
        private readonly IMapper _mapper;
        private readonly ILabelRepository _labelRepository;
        public LabelService(IMapper mapper, ILabelRepository labelRepository) 
        {
            _mapper = mapper;
            _labelRepository = labelRepository;
        }

        public async Task<LabelResponseDTO> GetLabelByIdAsync(int id)
        {
            Label label = await _labelRepository.GetLabelByIdAsync(id);
            return _mapper.Map<LabelResponseDTO>(label);
        }

        public async Task<LabelResponseDTO> createLabelAsync(LabelRequestDTO labelRequestDTO)
        {
            if (await _labelRepository.GetLabelByNameAsync(labelRequestDTO.Name) != null)
            {
                return null;
            }
            var label = _mapper.Map<Label>(labelRequestDTO);
            var createdLabel = await _labelRepository.CreateLabelAsync(label);
            return _mapper.Map<LabelResponseDTO>(createdLabel);
        }

        public async Task<IEnumerable<LabelResponseDTO>> GetAllLabelsAsync()
        {
            IEnumerable<Label> labels = await _labelRepository.GetAllLabelsAsync();
            return _mapper.Map<List<LabelResponseDTO>>(labels);
        }
    }
}
