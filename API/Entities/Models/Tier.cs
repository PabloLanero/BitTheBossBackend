using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BTB.Entities.Models
{
    public class Tier
    {
        [Key]
        [NotNull]
        public int Id {get;set;} = 0;
        [NotNull]
        public string Titulo {get;set; } = "";
        
        [NotNull]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion {get; set; }= DateTime.Now;
        [Column("Visible")]
        [NotNull]
        [DefaultValue(true)]
        public bool Visible = true;

        public List<Usuario>? UsuarioId {get; set; } = new List<Usuario>();

        /// Recuerda que EFCore necesita de los objetos constructores sin parametros
        public Tier () {}
    }
}