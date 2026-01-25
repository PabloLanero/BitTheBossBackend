namespace BTB.Entities.Models{
    public class Nodo {
        public byte IdNodo {get; set;}
        public Tropa[] ArrTropas {get; set; }= new Tropa[2];
        public Usuario? DuenoNodo {get; set; }
        public Nodo(){}
    }
}