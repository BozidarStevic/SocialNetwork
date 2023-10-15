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

        public async Task<CommentResponseDTO> GetCommentByIdAsync(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null) return null;

            return _mapper.Map<CommentResponseDTO>(comment);
        }

        public async Task<CommentResponseDTO> AddCommentAsync(string userId, int postId, string text)
        {
            var comment = new Comment
            {
                UserId = userId,
                User = await _userManager.FindByIdAsync(userId),
                PostId = postId,
                Post = await _postRepository.GetPostByIdAsync(postId),
                Text = text,
                TimePosted = DateTime.Now
            };
            Comment commentRet = await _commentRepository.AddCommentAsync(comment);
            return _mapper.Map<CommentResponseDTO>(commentRet);
        }

    }
}
