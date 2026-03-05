using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.Models
{
    public class Partida
    {
        [Key]
        public string IdPartida { get;set; } = "";
        public List<Usuario> ArrUsuario {get;set;} = new List<Usuario>();
        public List<Nodo> LstNodos {get;set;} = new List<Nodo>();
        public List<Movimiento> movimientos = new List<Movimiento>();
        public Partida () {}
    }
}