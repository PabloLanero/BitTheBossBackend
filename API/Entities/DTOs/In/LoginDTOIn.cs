using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.DTO
{
    public class LoginDtoIn
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}