namespace BTB.Entities.Models
{
    public class Movimiento
    {
        public int Id {get; set; }
        public Tropa Tropa {get;set; } = new Tropa();
        public Nodo NodoDestino {get; set; } = new Nodo();
        public Movimiento() {}
    }
}