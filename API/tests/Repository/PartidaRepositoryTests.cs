using BTB.Data;
using BTB.Entities.Models;
using BTB.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BTB.Tests.Repository
{
    public class PartidaRepositoryTests
    {
        private BTBContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BTBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new BTBContext(options);
        }

        [Fact]
        public async Task GetPartidasAsync_ReturnsAllPartidas()
        {
            using var context = GetInMemoryContext();
            context.Partidas.AddRange(
                new Partida { IdPartida = "P1" },
                new Partida { IdPartida = "P2" }
            );
            await context.SaveChangesAsync();

            var repo = new PartidaRepository(context);
            var result = repo.GetPartidasAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task PostPartidaAsync_AddsPartida()
        {
            using var context = GetInMemoryContext();
            var repo = new PartidaRepository(context);
            var newPartida = new Partida { IdPartida = "P1" };

            var result = await repo.PostPartidaAsync(newPartida);

            Assert.NotNull(result);
            Assert.Equal("P1", result.IdPartida);
            Assert.Equal(1, await context.Partidas.CountAsync());
        }

        [Fact]
        public async Task GetPartidaByIdAsync_ReturnsPartida()
        {
            using var context = GetInMemoryContext();
            context.Partidas.Add(new Partida { IdPartida = "P1" });
            await context.SaveChangesAsync();

            var repo = new PartidaRepository(context);
            var result = await repo.GetPartidaByIdAsync("P1");

            Assert.NotNull(result);
            Assert.Equal("P1", result.IdPartida);
        }
    }
}
