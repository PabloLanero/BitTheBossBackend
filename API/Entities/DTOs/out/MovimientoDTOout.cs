namespace BTB.Entities.DTO
{
    public class MovimientoDTOOut
    {
        public int Id { get; set; }
        public TropaDTOOut? Tropa { get; set; }
        public int NodoDestinoId { get; set; }
    }
}
