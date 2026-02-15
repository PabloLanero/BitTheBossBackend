using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.Models{
    public class Tropa {
        [Key]
        public int Id {get; set;}
        public string Nombre {get;set; } = "";
        public float Vida {get;set;} = 100f;
        public float Damage {get; set; } = 50f;
        public Tropa(){}
    }
}