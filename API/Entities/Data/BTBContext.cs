using BTB.Entities.Enums;
using BTB.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BTB.Data
{
    public class BTBContext : DbContext
    {

        public DbSet<Tier> Tiers {get; set; }
        public DbSet<Usuario> Usuarios {get; set; }
        public DbSet<Partida> Partidas {get; set; }
        public DbSet<Tropa> Tropas {get; set; }
        public DbSet<Nodo> Nodos {get; set; }
        public DbSet<Movimiento> Movimientos {get; set; }
        

        public BTBContext(DbContextOptions<BTBContext> options) : base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// Aqui genera datos de ejemplo nada mas tener contacto en la base de datos, ideal para 
            /// tener unos pocos datos
            
            Tier defaultTier = new Tier {Id= 1, Titulo="Bronce", FechaCreacion= DateTime.Now, Visible= true };

            modelBuilder.Entity<Tier>().HasData(
                defaultTier,
                new Tier {Id= 2, Titulo="Plata", FechaCreacion= DateTime.Now, Visible= true },
                new Tier {Id= 3, Titulo="Oro", FechaCreacion= DateTime.Now, Visible= true }
            );

            // /// Ahora le podemos indicar distintas propiedades de la columna
            // modelBuilder.Entity<Tier>().Property(s => s.Id).IsRequired();

            // /// Para hacerlo de manera fluida
            // modelBuilder.Entity<Tier>().Property(s => s.FechaCreacion)
            //     .HasDefaultValue(DateTime.Now)
            //     .HasColumnType("DateTime");
            //     //...

            // /// Aqui le indica que la columna del id debe tener como nombre "IdTier", sino 
            // /// se llamara solo Id
            // modelBuilder.Entity<Tier>().Property(s => s.Id).HasColumnName("IdTier");
            // ///Aqui le indicamos el nombre de la tabla, sino seria solo Tier
            // modelBuilder.Entity<Tier>().ToTable("Tier");

            // // Usuario
            // modelBuilder.Entity<Usuario>().Property(s => s.Tier)
            //     .HasDefaultValue(1);
            // modelBuilder.Entity<Usuario>().HasData(
            //     new Usuario { UsuarioId = 1, Nombre = "Ejemplo", Correo = "Jhon@gmail.com", Password = "asd", FechaCreacion = DateTime.Now, Visible = true, Rol = Roles.Admin, Tier = defaultTier },
            //     new Usuario { UsuarioId = 2, Nombre = "Ejemplo2", Correo = "Mary@gmail.com", Password = "asdasd", FechaCreacion = DateTime.Now, Visible = true, Rol = Roles.Admin, Tier = defaultTier }
            // );
            
            
            /// Tambien puede recoger las etiquetas de las propiedades
            /// como [Key] y mas
        }

    }
}