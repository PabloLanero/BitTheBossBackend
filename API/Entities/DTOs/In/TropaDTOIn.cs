using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.DTO
{
    public class TropaDTOIn
    {
        [Required]
        public string? Nombre { get; set; }

        public float Vida { get; set; } = 100f;

        public float Damage { get; set; } = 50f;
    }
}
