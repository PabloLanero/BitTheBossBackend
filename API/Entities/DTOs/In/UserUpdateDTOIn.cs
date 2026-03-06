using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.DTO
{
    public class UserUpdateDTOIn
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
