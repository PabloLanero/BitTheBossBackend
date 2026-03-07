using BTB.Data;
using BTB.Entities.Models;
using BTB.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BTB.Tests.Repository
{
    public class MovimientoRepositoryTests
    {
        private BTBContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BTBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new BTBContext(options);
        }

        [Fact]
        public async Task GetMovimientosAsync_ReturnsAllMovimientos()
        {
            using var context = GetInMemoryContext();
            context.Movimientos.AddRange(
                new Movimiento { Id = 1 },
                new Movimiento { Id = 2 }
            );
            await context.SaveChangesAsync();

            var repo = new MovimientoRepository(context);
            var result = repo.GetMovimientosAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task PostMovimientoAsync_AddsMovimiento()
        {
            using var context = GetInMemoryContext();
            var repo = new MovimientoRepository(context);
            var newMovimiento = new Movimiento { };

            var result = await repo.PostMovimientoAsync(newMovimiento);

            Assert.NotNull(result);
            Assert.Equal(1, await context.Movimientos.CountAsync());
        }

        [Fact]
        public async Task GetMovimientoByIdAsync_ReturnsMovimiento()
        {
            using var context = GetInMemoryContext();
            context.Movimientos.Add(new Movimiento { Id = 1 });
            await context.SaveChangesAsync();

            var repo = new MovimientoRepository(context);
            var result = await repo.GetMovimientoByIdAsync(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteMovimientoAsync_RemovesMovimiento()
        {
            using var context = GetInMemoryContext();
            context.Movimientos.Add(new Movimiento { Id = 1 });
            await context.SaveChangesAsync();

            var repo = new MovimientoRepository(context);
            var result = await repo.DeleteMovimientoAsync(1);

            Assert.True(result);
        }
    }
}
