using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTB.Entities.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Nombre {get;set;} = "";
        public string Correo {get;set; } = "";
        public string Password {get; set; } = "";
        public bool Visible {get; set; }= true;
        public DateTime FechaCreacion {get; set; } = DateTime.Now;
        [ForeignKey("Usuario_Tier")]
        public Tier Tier {get;set;} = new Tier();
        public List<Partida>? LstPartidas {get; set; } = new List<Partida>();
        public Usuario(){}

    }
}