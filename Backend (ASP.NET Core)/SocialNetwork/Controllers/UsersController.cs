using Azure.Core;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet("Follow/{followedUserId}")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> FollowUser(string followedUserId)
        {
            var follower = await _userManager.GetUserAsync(User);
            var followed = await _userManager.FindByIdAsync(followedUserId);

            if (follower == null || followed == null)
            {
                return BadRequest("Jedan od korisnika ne postoji u bazi.");
            }

            var result = await _userService.FollowUserAsync(follower.Id, followed.Id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Neuspešno praćenje korisnika.");
            }
        }

        [HttpGet("UnfollowUser/{unfollowedUserId}")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> UnfollowUser(string unfollowedUserId)
        {
            var follower = await _userManager.GetUserAsync(User);
            var unfollowed = await _userManager.FindByIdAsync(unfollowedUserId);

            if (follower == null || unfollowed == null)
            {
                return BadRequest("Jedan od korisnika ne postoji u bazi.");
            }

            var result = await _userService.UnfollowUserAsync(follower.Id, unfollowed.Id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Neuspešno otpraćenje korisnika.");
            }
        }

    }
}
