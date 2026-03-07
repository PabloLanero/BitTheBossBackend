using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service;
using BTB.Service.Common;
using Moq;
using Xunit;

namespace BTB.Tests.Service
{
    public class TierServiceTests
    {
        [Fact]
        public async Task GetTiersAsync_ReturnsListOfDTOs()
        {
            var mockRepo = new Mock<ITierRepository>();
            mockRepo.Setup(r => r.GetTiersAsync()).ReturnsAsync(new List<Tier>
            {
                new Tier { Id = 1, Titulo = "Bronce", Visible = true }
            });

            var service = new TierService(mockRepo.Object);
            var result = await service.GetTiersAsync();

            Assert.Single(result);
            Assert.Equal("Bronce", result.First().Titulo);
        }

        [Fact]
        public async Task AddTierAsync_ThrowsValidationException_WhenDtoIsNull()
        {
            var mockRepo = new Mock<ITierRepository>();
            var service = new TierService(mockRepo.Object);

            await Assert.ThrowsAsync<ValidationException>(() => service.AddTierAsync(null!));
        }

        [Fact]
        public async Task DeleteTierAsync_ThrowsValidationException_WhenIdIsInvalid()
        {
            var mockRepo = new Mock<ITierRepository>();
            var service = new TierService(mockRepo.Object);

            await Assert.ThrowsAsync<ValidationException>(() => service.DeleteTierAsync(0));
        }
    }
}
