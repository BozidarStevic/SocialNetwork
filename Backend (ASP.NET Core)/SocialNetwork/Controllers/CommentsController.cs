using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.DTOs;
using SocialNetwork.Exceptions;
using SocialNetwork.Models;
using SocialNetwork.Services;
using SocialNetwork.Services.IServices;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        private readonly IPostService _postService;

        public CommentsController(ICommentService commentService, UserManager<User> userManager, IPostService postService)
        {
            _commentService = commentService;
            _userManager = userManager;
            _postService = postService;
        }

        

    }
}
