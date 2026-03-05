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

            /// Ahora le podemos indicar distintas propiedades de la columna
            // modelBuilder.Entity<Tier>().Property(s => s.Id).IsRequired();

            /// Para hacerlo de manera fluida
            // modelBuilder.Entity<Tier>().Property(s => s.FechaCreacion)
            //     .HasDefaultValue(DateTime.Now)
            //     .HasColumnType("DateTime");
            //     //...

            /// Aqui le indica que la columna del id debe tener como nombre "IdTier", sino 
            /// se llamara solo Id
            // modelBuilder.Entity<Tier>().Property(s => s.Id).HasColumnName("IdTier");
            ///Aqui le indicamos el nombre de la tabla, sino seria solo Tier
            // modelBuilder.Entity<Tier>().ToTable("Tier");

            // Esto lo he copiado de la documentacion de EFCore para ver si se me arregla 
            // las relaciones
            /// El enlace es este 
            /// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
            modelBuilder.Entity<Tier>()
                .HasMany(e => e.UsuarioId)
                .WithOne(e => e.Tier)
                .HasForeignKey(e => e.TierId);


            /// A partir de aqui, generado por Claudio
            // Usuario - Tier relationship
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Tier)
                .WithMany(t => t.UsuarioId)
                .HasForeignKey("TierId");
            

            // Usuario - Partida many-to-many relationship
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Partidas)
                .WithMany(p => p.ArrUsuario)
                .UsingEntity("UsuarioPartida");

            /// Mira, Claudio a decidido utilizar objetos genericos para tiparlos, esa es buena
            modelBuilder.Entity<Usuario>().HasData(
                new { UsuarioId = 1, Nombre = "Ejemplo", Correo = "Jhon@gmail.com", Password = "asd", FechaCreacion = DateTime.Now, Visible = true, Rol = Roles.Admin, TierId = 1 },
                new { UsuarioId = 2, Nombre = "Ejemplo2", Correo = "Mary@gmail.com", Password = "asdasd", FechaCreacion = DateTime.Now, Visible = true, Rol = Roles.Admin, TierId = 1 },
                new { UsuarioId = 3, Nombre = "Player1", Correo = "player1@gmail.com", Password = "pass123", FechaCreacion = DateTime.Now, Visible = true, Rol = Roles.Usuario, TierId = 2 },
                new { UsuarioId = 4, Nombre = "Player2", Correo = "player2@gmail.com", Password = "pass456", FechaCreacion = DateTime.Now, Visible = true, Rol = Roles.Usuario, TierId = 3 }
            );

            // Partida relationships
            modelBuilder.Entity<Partida>()
                .HasMany(p => p.LstNodos)
                .WithOne()
                .HasForeignKey("PartidaIdPartida");

            modelBuilder.Entity<Partida>()
                .HasMany(p => p.movimientos)
                .WithOne(m => m.Partida)
                .HasForeignKey("PartidaIdPartida");

            modelBuilder.Entity<Partida>().HasData(
                new Partida { IdPartida = "partida-001" },
                new Partida { IdPartida = "partida-002" }
            );

            // Movimiento relationships
            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Tropa)
                .WithMany()
                .HasForeignKey("TropaId");

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.NodoDestino)
                .WithMany()
                .HasForeignKey("NodoDestinoIdNodo");

            // Tropa
            modelBuilder.Entity<Tropa>().HasData(
                new Tropa { Id = 1, Nombre = "Triangulo", Vida = 100f, Damage = 50f },
                new Tropa { Id = 2, Nombre = "Cuadrado", Vida = 100f, Damage = 50f },
                new Tropa { Id = 3, Nombre = "Circulo", Vida = 100f, Damage = 50f }
            );

            // Nodo relationships
            modelBuilder.Entity<Nodo>()
                .HasOne(n => n.DuenoNodo)
                .WithMany()
                .HasForeignKey("DuenoNodoUsuarioId")
                .IsRequired(false);

            modelBuilder.Entity<Nodo>().Ignore(n => n.ArrTropas);

            modelBuilder.Entity<Nodo>().HasData(
                new Nodo { IdNodo = 1 },
                new Nodo { IdNodo = 2 },
                new Nodo { IdNodo = 3 }
            );
            
            
            /// Tambien puede recoger las etiquetas de las propiedades
            /// como [Key] y mas
            


        }

    }
}