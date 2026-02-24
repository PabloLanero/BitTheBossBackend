using System.ComponentModel.DataAnnotations;
using BTB.Entities.Models;

namespace BTB.Entities.DTO
{
    
    public class PartidaDTOIn
    {
        [Required]
        public string? IdPartida { get; set; }

        public int[]? ArrUsuario { get; set; } = new int[2];

        public List<int>? LstNodos { get; set; } = new List<int>();
    }
}
