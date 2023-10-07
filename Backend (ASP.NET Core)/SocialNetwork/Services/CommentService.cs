using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DTOs;
using SocialNetwork.Exceptions;
using SocialNetwork.Models;
using SocialNetwork.Repositories.IRepositories;
using SocialNetwork.Services.IServices;

namespace SocialNetwork.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, UserManager<User> userManager, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userManager = userManager;
            _postRepository = postRepository;
        }

        

    }
}
