using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Services.IServices;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRateService _rateService;
        private readonly UserManager<User> _userManager;

        public RatesController(IRateService rateService, UserManager<User> userManager)
        {
            _rateService = rateService;
            _userManager = userManager;
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost("{postId}/rate/{rateValue}")]
        public async Task<ActionResult<PostResponseDTO>> RatePost(int postId, int rateValue)
        {
            var user = await _userManager.GetUserAsync(User);
            var postResponseDTO = await _rateService.RatePostAsync(user, postId, rateValue);
            return Ok(postResponseDTO);
        }

    }
}
