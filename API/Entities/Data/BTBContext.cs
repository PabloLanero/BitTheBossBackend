using BTB.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BTB.Data
{
    public class BTBContext : DbContext
    {

        public DbSet<Tier> Tiers {get; set; }
        public DbSet<Usuario> Usuarios {get; set; }
        public DbSet<Partida> Partidas {get; set; }
        

        public BTBContext(DbContextOptions<BTBContext> options) : base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// Aqui genera datos de ejemplo nada mas tener contacto en la base de datos, ideal para 
            /// tener unos pocos datos
            /// 
            modelBuilder.Entity<Tier>().HasData(
                new Tier {Id= 1, Titulo="Bronce", FechaCreacion= DateTime.Now, Visible= true },
                new Tier {Id= 2, Titulo="Plata", FechaCreacion= DateTime.Now, Visible= true },
                new Tier {Id= 3, Titulo="Oro", FechaCreacion= DateTime.Now, Visible= true }
            );

            // modelBuilder.Entity<Usuario>().HasData(
            //     new Usuario {Id= 1, Nombre="Ejemplo", Correo="Jhon@gmail.com", Password="asd",
            //     Tier= new Tier{Id=1}, FechaCreacion= DateTime.Now, Visible= true }
            // );
            /// Aqui le indica que la columna del id debe tener como nombre "IdTier", sino 
            /// se llamara solo Id
            modelBuilder.Entity<Tier>().Property(s => s.Id).HasColumnName("IdTier");
            ///Aqui le indicamos el nombre de la tabla, sino seria solo Tier
            modelBuilder.Entity<Tier>().ToTable("Tiers");

            /// Ahora le podemos indicar distintas propiedades de la columna
            modelBuilder.Entity<Tier>().Property(s => s.Id).IsRequired();

            /// Para hacerlo de manera fluida
            modelBuilder.Entity<Tier>().Property(s => s.FechaCreacion)
                .HasDefaultValue(DateTime.Now)
                .HasColumnType("DateTime");
                //...
            // modelBuilder.Entity<Usuario>().Property(s => s.Tier)
            //     .HasDefaultValue(1);
            
            /// Tambien puede recoger las etiquetas de las propiedades
            /// como [Key] y mas
        }

    }
}