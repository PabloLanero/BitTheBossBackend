using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.DTO
{
    public class TierDTOIn
    {
        [Required]
        public string? Titulo { get; set; }

        public bool Visible { get; set; } = true;
    }
}
