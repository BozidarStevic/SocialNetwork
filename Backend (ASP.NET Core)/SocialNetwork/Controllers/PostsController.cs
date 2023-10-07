﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Repositories;
using SocialNetwork.Repositories.IRepositories;
using SocialNetwork.Services;
using SocialNetwork.Services.IServices;
using System.Security.Claims;
using SocialNetwork.Exceptions;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;

        public PostsController(IPostService postService, UserManager<User> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        

    }
}
