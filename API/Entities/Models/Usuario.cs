using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BTB.Entities.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; } = 0;
        [NotNull]
        public string Nombre {get;set;} = "";
        [EmailAddress]
        [NotNull]
        public string Correo {get;set; } = "";
        [NotNull]
        [PasswordPropertyText]
        public string Password {get; set; } = "";
        public string? imageUrl {get;set;}
        public bool Visible {get; set; }= true;
        public DateTime FechaCreacion {get; set; } = DateTime.Now;
        [NotNull]
        public string Rol { get;set; } = string.Empty;
        [DefaultValue(1)]
        public int TierId {get; set; } = 1; // Por EFCore
        public Tier? Tier {get;set;} 
        public List<Partida>? Partidas {get; set; } = new List<Partida>();
        public Usuario(){}

    }
}