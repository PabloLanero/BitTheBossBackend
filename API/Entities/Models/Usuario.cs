using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTB.Entities.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; } = 0;
        public string Nombre {get;set;} = "";
        public string Correo {get;set; } = "";
        public string Password {get; set; } = "";
        public bool Visible {get; set; }= true;
        public DateTime FechaCreacion {get; set; } = DateTime.Now;

        public int TierId {get;set;} = 1;
        public List<Partida>? Partidas {get; set; } = new List<Partida>();
        public Usuario(){}

    }
}