using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DTOs
{
    public class UpdatePostDTO
    {
        [MinLength(1)]
        public string? Text { get; set; }
        [MinLength(1)]
        public string? Location { get; set; }
        public List<int>? LabelsIdsForDelete { get; set; }
        public List<int>? NewLabelsIds { get; set; }

        [MaxLength(4)]
        public List<int>? AttachmentsIdsForDelete { get; set; }

        [Display(Name = "Choose files for Attachments")]
        [MaxLength(4, ErrorMessage = "Maksimalno dozvoljen broj fajlova je 4.")]
        public IFormFileCollection? AttachemtsFiles { get; set; }
    }
}
