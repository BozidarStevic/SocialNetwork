using AutoMapper;
using SocialNetwork.Data;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Repositories;
using SocialNetwork.Repositories.IRepositories;
using SocialNetwork.Services.IServices;

namespace SocialNetwork.Services
{
    public class RateService : IRateService
    {
        private readonly IPostRepository _postRepository;
        private readonly IRateRepository _rateRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RateService(IPostRepository postRepository, IRateRepository rateRepository, AppDbContext context, IMapper mapper)
        {
            _postRepository = postRepository;
            _rateRepository = rateRepository;
            _context = context;
            _mapper = mapper;
        }
        public async Task<PostResponseDTO> RatePostAsync(User user, int postId, int rateValue)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            var rate = new Rate {
                PostId = postId,
                Post = post,
                UserId = user.Id,
                User = user,
                RateValue = rateValue
            };
            await _rateRepository.CreateRateAsync(rate);

            var ratesList = await _rateRepository.FindRatesByPostIdAsync(postId);

            double ratesCount = ratesList.Count();

            double ratesSum = 0;
            foreach (var currRate in ratesList)
            {
                ratesSum += currRate.RateValue;
            }
            double newAvgRate = 0;
            if (ratesCount != 0)
            {
                newAvgRate = ratesSum / ratesCount;
                newAvgRate = Math.Round(newAvgRate, 2);
            }
            post.AverageRate = newAvgRate;
            await _context.SaveChangesAsync();

            return _mapper.Map<PostResponseDTO>(post);
        }
    }
}
