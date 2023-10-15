using SocialNetwork.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DTOs
{
    public class PostRequestDTO
    {
        [Required]
        [MinLength(1)]
        public string? Text { get; set; }
        
        public string? Location { get; set; }

        public List<int>? labelsIds { get; set; }

        [Display(Name = "Choose files for Attachments")]
        [MaxLength(4, ErrorMessage = "Maksimalno dozvoljen broj fajlova je 4.")]
        public IFormFileCollection? AttachemtsFiles { get; set; }


    }
}
