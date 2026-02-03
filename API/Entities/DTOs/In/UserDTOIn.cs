using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.DTO
{
    public class UserDTOIn
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}