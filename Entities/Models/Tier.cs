namespace BTB.Entities.Models
{
    public class Tier
    {
        public byte Id {get;set;} = 0;
        public string Titulo {get;set; } = "";
        public DateTime FechaCreacion {get; set; }= DateTime.Now;
        public bool Visible = true;
        public List<Usuario> LstUsuarios {get; set; } = new List<Usuario>();
    }
}