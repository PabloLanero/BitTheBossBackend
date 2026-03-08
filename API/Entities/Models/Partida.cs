using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTB.Entities.Models
{
    public class Partida
    {
        [Key]
        public string IdPartida { get;set; } = "";
        [NotMapped]
        public List<Usuario> ArrUsuario {get;set;} = new List<Usuario>();
        public List<Nodo>? LstNodos {get;set;} = new List<Nodo>();
        public List<Movimiento>? movimientos = new List<Movimiento>();
        public Partida () {}
    }
}
