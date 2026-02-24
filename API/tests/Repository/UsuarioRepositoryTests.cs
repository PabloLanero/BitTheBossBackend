using BTB.Data;
using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BTB.Tests.Repository
{
    public class UsuarioRepositoryTests
    {
        private BTBContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BTBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new BTBContext(options);
        }

        [Fact]
        public async Task GetUsuariosAsync_ReturnsVisibleUsers()
        {
            using var context = GetInMemoryContext();
            context.Usuarios.AddRange(
                new Usuario { UsuarioId = 1, Nombre = "User1", Visible = true },
                new Usuario { UsuarioId = 2, Nombre = "User2", Visible = false }
            );
            await context.SaveChangesAsync();

            var repo = new UsuarioRepository(context);
            var result = repo.GetUsuariosAsync();

            Assert.Single(result);
            Assert.Equal("User1", result[0].Nombre);
        }

        [Fact]
        public async Task PostUsuarioAsync_AddsUser()
        {
            using var context = GetInMemoryContext();
            var repo = new UsuarioRepository(context);
            var newUser = new Usuario { Nombre = "Test", Correo = "test@test.com" };

            var result = await repo.PostUsuarioAsync(newUser);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Nombre);
            Assert.Equal(1, await context.Usuarios.CountAsync());
        }

        [Fact]
        public async Task GetUsuarioByIdAsync_ReturnsUser()
        {
            using var context = GetInMemoryContext();
            context.Usuarios.Add(new Usuario { UsuarioId = 1, Nombre = "User1" });
            await context.SaveChangesAsync();

            var repo = new UsuarioRepository(context);
            var result = await repo.GetUsuarioByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("User1", result.Nombre);
        }
    }
}
