namespace BTB.Entities.DTO
{
    public class PartidaDTOOut
    {
        public string? IdPartida { get; set; }
        public List<UsuarioRefDTOOut>? ArrUsuario { get; set; } = new List<UsuarioRefDTOOut>();
        public List<NodoDTOOut>? LstNodos { get; set; } = new List<NodoDTOOut>();
    }
}