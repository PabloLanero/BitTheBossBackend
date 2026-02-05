using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.DTO
{
    
    public class PartidaDTOIn
    {
        [Required]
        public string? IdPartida { get; set; }

        public List<UsuarioRefDTO>? ArrUsuario { get; set; } = new List<UsuarioRefDTO>();

        public List<NodoDTOIn>? LstNodos { get; set; } = new List<NodoDTOIn>();
    }
}
