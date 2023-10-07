using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Repositories;
using SocialNetwork.Repositories.IRepositories;
using SocialNetwork.Services.IServices;
using static System.Net.Mime.MediaTypeNames;
using SocialNetwork.Exceptions;

namespace SocialNetwork.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public PostService(IPostRepository postRepository, IMapper mapper, UserManager<User> userManager)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        

    }
}
