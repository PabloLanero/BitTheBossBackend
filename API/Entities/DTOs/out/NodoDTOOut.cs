using System.Collections.Generic;

namespace BTB.Entities.DTO
{
    public class NodoDTOOut
    {
        public int IdNodo { get; set; }
        public List<TropaDTOOut>? ArrTropas { get; set; } = new List<TropaDTOOut>();
        public UsuarioRefDTOOut? DuenoNodo { get; set; }
    }
}
