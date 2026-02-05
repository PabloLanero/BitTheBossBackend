using System.Collections.Generic;

namespace BTB.Entities.DTO
{
    public class NodoDTOIn
    {
        public int IdNodo { get; set; }
        public List<TropaDTOIn>? ArrTropas { get; set; } = new List<TropaDTOIn>();
        public UsuarioRefDTO? DuenoNodo { get; set; }
    }
}
