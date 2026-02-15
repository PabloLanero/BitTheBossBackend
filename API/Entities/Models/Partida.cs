using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.Models
{
    public class Partida
    {
        [Key]
        public string IdPartida { get;set; } = "";
        public Usuario[] ArrUsuario {get;set;} = new Usuario[2];
        public List<Nodo> LstNodos {get;set;} = new List<Nodo>();
        public Partida () {}
    }
}