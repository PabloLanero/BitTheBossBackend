using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BTB.Entities.Models
{
    public class Tier
    {
        [Key]
        [NotNull]
        public int Id {get;set;} = 0;
        public string Titulo {get;set; } = "";
        public DateTime FechaCreacion {get; set; }= DateTime.Now;
        public bool Visible = true;
        public List<Usuario> LstUsuarios {get; set; } = new List<Usuario>();

        /// Recuerda que EFCore necesita de los objetos constructores sin parametros
        public Tier () {}
    }
}