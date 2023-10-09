using SocialNetwork.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DTOs
{
    public class PostRequestDTO
    {
        [Required]
        public string? Text { get; set; }
        [Required]
        public string? Location { get; set; }

        public List<int>? labelsIds { get; set; }

        [Display(Name = "Choose the gallery images of your book")]
        public IFormFileCollection? AttachemtsFiles { get; set; }


    }
}
