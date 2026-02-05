using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.DTO
{
    public class MovimientoDTOIn
    {
        [Required]
        public TropaDTOIn? Tropa { get; set; }

        [Required]
        public int NodoDestinoId { get; set; }
    }
}
