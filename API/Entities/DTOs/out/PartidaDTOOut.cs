namespace BTB.Entities.DTO
{
    public class TropaDTOOut
    {
        public string? Nombre { get; set; }
        public float Vida { get; set; }
        public float Damage { get; set; }
    }

    public class UsuarioRefDTOOut
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
    }

    public class NodoDTOOut
    {
        public int IdNodo { get; set; }
        public List<TropaDTOOut>? ArrTropas { get; set; } = new List<TropaDTOOut>();
        public UsuarioRefDTOOut? DuenoNodo { get; set; }
    }

    public class PartidaDTOOut
    {
        public string? IdPartida { get; set; }
        public List<UsuarioRefDTOOut>? ArrUsuario { get; set; } = new List<UsuarioRefDTOOut>();
        public List<NodoDTOOut>? LstNodos { get; set; } = new List<NodoDTOOut>();
    }
}