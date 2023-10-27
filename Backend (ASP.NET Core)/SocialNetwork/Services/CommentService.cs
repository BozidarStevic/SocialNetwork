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

        public async Task UpdateCommentTextAsync(string currentUserId, int commentId, string newText)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            if (comment == null)
            {
                throw new NotFoundException("Komentar nije pronađen.");
            }

            if (comment.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Nemate dozvolu da ažurirate ovaj komentar.");
            }
            comment.Text = newText;

            await _commentRepository.UpdateCommentAsync(comment);
        }

        public async Task<bool> DeleteCommentAsync(int commentId, string userId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);
            var user = await _userManager.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || comment == null || user.Posts == null || comment.Post == null || comment.User == null)
            {
                return false;
            }
            bool commentOnUsersPost = false;
            foreach (var post in user.Posts)
            {
                if (post.Id == comment.Post.Id)
                {
                    commentOnUsersPost = true;
                }
            }

            if (comment.User.Id == userId || commentOnUsersPost)
            {
                await _commentRepository.DeleteCommentAsync(comment);
                return true;
            }

            return false;
        }

    }
}
