using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.DTO
{
    public class TropaDTOIn
    {
        [Required]
        public string? Nombre { get; set; }
        public float Vida { get; set; }
        public float Damage { get; set; }
    }

    public class UsuarioRefDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
    }

    public class NodoDTOIn
    {
        public int IdNodo { get; set; }
        public List<TropaDTOIn>? ArrTropas { get; set; } = new List<TropaDTOIn>();
        public UsuarioRefDTO? DuenoNodo { get; set; }
    }

    public class PartidaDTOIn
    {
        [Required]
        public string? IdPartida { get; set; }

        public List<UsuarioRefDTO>? ArrUsuario { get; set; } = new List<UsuarioRefDTO>();

        public List<NodoDTOIn>? LstNodos { get; set; } = new List<NodoDTOIn>();
    }
}
