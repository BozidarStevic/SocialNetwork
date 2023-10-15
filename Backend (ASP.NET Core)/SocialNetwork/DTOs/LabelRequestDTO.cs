using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DTOs
{
    public class LabelRequestDTO
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
