using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Repositories;
using SocialNetwork.Repositories.IRepositories;
using SocialNetwork.Services.IServices;

namespace SocialNetwork.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly UserManager<User> _userManager;
        public LikeService(ILikeRepository likeRepository, IMapper mapper, IPostRepository postRepository, UserManager<User> userManager)
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
            _postRepository = postRepository;
            _userManager = userManager;
        }

        

    }
}
