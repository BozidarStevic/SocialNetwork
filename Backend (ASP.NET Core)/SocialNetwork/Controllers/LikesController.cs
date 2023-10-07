using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Repositories;
using SocialNetwork.Repositories.IRepositories;
using SocialNetwork.Services;
using SocialNetwork.Services.IServices;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly UserManager<User> _userManager;
        private readonly IPostService _postService;

        public LikesController(ILikeService likeService, UserManager<User> userManager, IPostService postService)
        {
            _likeService = likeService;
            _userManager = userManager;
            _postService = postService;
        }

        
    }
}
