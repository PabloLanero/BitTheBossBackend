using BTB.Data;
using BTB.Entities.Models;
using BTB.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BTB.Tests.Repository
{
    public class TierRepositoryTests
    {
        private BTBContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BTBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new BTBContext(options);
        }

        [Fact]
        public async Task GetTiersAsync_ReturnsAllTiers()
        {
            using var context = GetInMemoryContext();
            context.Tiers.AddRange(
                new Tier { Id = 1, Titulo = "Bronce" },
                new Tier { Id = 2, Titulo = "Plata" }
            );
            await context.SaveChangesAsync();

            var repo = new TierRepository(context);
            var result = await repo.GetTiersAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task PostTierAsync_AddsTier()
        {
            using var context = GetInMemoryContext();
            var repo = new TierRepository(context);
            var newTier = new Tier { Titulo = "Oro" };

            var result = await repo.PostTierAsync(newTier);

            Assert.NotNull(result);
            Assert.Equal("Oro", result.Titulo);
            Assert.Equal(1, await context.Tiers.CountAsync());
        }

        [Fact]
        public async Task GetTierByIdAsync_ReturnsTier()
        {
            using var context = GetInMemoryContext();
            context.Tiers.Add(new Tier { Id = 1, Titulo = "Bronce" });
            await context.SaveChangesAsync();

            var repo = new TierRepository(context);
            var result = await repo.GetTierByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Bronce", result.Titulo);
        }

        [Fact]
        public async Task DeleteTierAsync_RemovesTier()
        {
            using var context = GetInMemoryContext();
            context.Tiers.Add(new Tier { Id = 1, Titulo = "Bronce" });
            await context.SaveChangesAsync();

            var repo = new TierRepository(context);
            var result = await repo.DeleteTierAsync(1);

            Assert.True(result);
        }
    }
}
