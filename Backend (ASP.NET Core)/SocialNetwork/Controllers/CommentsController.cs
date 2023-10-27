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

        [Authorize(Roles = Roles.User)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentResponseDTO>> GetComment(int id)
        {
            var commentResponseDTO = await _commentService.GetCommentByIdAsync(id);

            if (commentResponseDTO == null)
            {
                return NotFound();
            }

            return Ok(commentResponseDTO);
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost("commentPost/{postId}")]
        public async Task<ActionResult<CommentResponseDTO>> CommentPost([FromBody] CommentPostRequestDTO request, int postId)
        {
            var user = await _userManager.GetUserAsync(User);
            var post = await _postService.GetPostByIdAsync(postId);

            if (!ModelState.IsValid || user == null || post == null)
            {
                return BadRequest("Korisnik nije ulogovan ili post ne postoji u bazi ili je prazan komentar!");
            }

            var commentResponseDTO = await _commentService.AddCommentAsync(user.Id, post.Id, request.Text);

            if (commentResponseDTO != null)
            {
                return CreatedAtAction(nameof(GetComment), new { id = commentResponseDTO.Id }, commentResponseDTO);
            }
            else
            {
                return BadRequest("Neuspešno ostavljanje komentara na post.");
            }
        }

        [Authorize(Roles = Roles.User)]
        [HttpPatch("{commentId}")]
        public async Task<IActionResult> UpdateCommentText(int commentId, [FromBody] CommentRequestDTO commentRequestDTO)
        {
            if (commentRequestDTO.Text == null || commentRequestDTO.Text.Equals(""))
            {
                return BadRequest("Nema teksta za izmenu!");
            }
            try
            {
                var currentUserId = _userManager.GetUserId(User);
                await _commentService.UpdateCommentTextAsync(currentUserId, commentId, commentRequestDTO.Text);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Došlo je do greške prilikom ažuriranja komentara.");
            }
        }

        [HttpDelete("User/{commentId}")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Korisnik nije ulogovan!");
            }

            var result = await _commentService.DeleteCommentAsync(commentId, user.Id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Brisanje komentara nije uspelo ili nemate pravo da obrišete ovaj komentar.");
            }
        }

    }
}
