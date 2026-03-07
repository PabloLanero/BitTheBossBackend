using BTB.Data;
using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BTB.Tests.Repository
{
    public class NodoRepositoryTests
    {
        private BTBContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BTBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new BTBContext(options);
        }

        [Fact]
        public async Task GetNodosAsync_ReturnsAllNodos()
        {
            using var context = GetInMemoryContext();
            context.Nodos.AddRange(
                new Nodo { IdNodo = 1 },
                new Nodo { IdNodo = 2 }
            );
            await context.SaveChangesAsync();

            var repo = new NodoRepository(context);
            var result = repo.GetNodosAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetNodoByIdAsync_ReturnsNodo()
        {
            using var context = GetInMemoryContext();
            context.Nodos.Add(new Nodo { IdNodo = 1 });
            await context.SaveChangesAsync();

            var repo = new NodoRepository(context);
            var result = await repo.GetNodoByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.IdNodo);
        }

        [Fact]
        public async Task DeleteNodoAsync_RemovesNodo()
        {
            using var context = GetInMemoryContext();
            context.Nodos.Add(new Nodo { IdNodo = 1 });
            await context.SaveChangesAsync();

            var repo = new NodoRepository(context);
            var result = await repo.DeleteNodoAsync(1);

            Assert.True(result);
        }
    }
}
