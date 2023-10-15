using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Repositories.IRepositories;
using SocialNetwork.Services;
using SocialNetwork.Services.IServices;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelService _labelService;

        public LabelsController(ILabelService labelService) 
        {
            _labelService = labelService;
        }

        [Authorize(Roles = Roles.User)]
        [HttpGet("{id}")]
        public async Task<ActionResult<LabelResponseDTO>> GetLabel(int id)
        {
            var labelResponseDTO = await _labelService.GetLabelByIdAsync(id);

            if (labelResponseDTO == null)
            {
                return NotFound();
            }

            return Ok(labelResponseDTO);
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<LabelResponseDTO>> CreateLabel(LabelRequestDTO labelRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var labelResponseDTO = await _labelService.createLabelAsync(labelRequestDTO);

            if (labelResponseDTO == null)
            {
                return BadRequest("Labela sa ovim imenom vec postoji!");
            }

            return CreatedAtAction(nameof(GetLabel), new { id = labelResponseDTO.Id }, labelResponseDTO);
        }

    }
}
