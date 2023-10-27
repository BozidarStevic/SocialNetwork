using Microsoft.AspNetCore.Authorization;
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
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        [Authorize(Roles = Roles.User)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponseDTO>> GetPost(int id)
        {
            var postResponseDTO = await _postService.GetPostByIdAsync(id);

            if (postResponseDTO == null)
            {
                return NotFound();
            }

            return Ok(postResponseDTO);
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost]
        public async Task<ActionResult<PostResponseDTO>> CreatePost([FromForm] PostRequestDTO postRequestDTO)
        {
            if (postRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Korisnik nije ulogovan.");
            }

            var postResponseDTO = await _postService.CreatePostAsync(postRequestDTO, user);

            if (postResponseDTO == null)
            {
                return UnprocessableEntity();
            }
            return CreatedAtAction(nameof(GetPost), new { id = postResponseDTO.Id }, postResponseDTO);
        }

        [Authorize(Roles = Roles.User)]
        [HttpGet("latest")]
        public async Task<ActionResult<List<PostResponseDTO>>> GetAllPostsSortedByDateTime()
        {
            var user = await _userManager.GetUserAsync(User);
            var postDTOs = await _postService.GetAllPostDTOsSortedByDateTimeAsync(user);

            return (postDTOs != null) ? Ok(postDTOs) : NotFound("Nema trazenih postova!");
        }

        [Authorize(Roles = Roles.User)]
        [HttpPut("update/{postId}")]
        public async Task<ActionResult<PostResponseDTO>> UpdatePost(int postId, [FromForm] UpdatePostDTO updatePostDTO)
        {
            if (updatePostDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var currentUserId = _userManager.GetUserId(User);
                var uadatedPost = await _postService.UpdatePostAsync(currentUserId,postId, updatePostDTO);
                return Ok(uadatedPost);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
