using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service;
using BTB.Service.Common;
using Moq;
using Xunit;

namespace BTB.Tests.Service
{
    public class NodoServiceTests
    {
        [Fact]
        public async Task GetNodosAsync_ReturnsListOfDTOs()
        {
            var mockRepo = new Mock<INodoRepository>();
            mockRepo.Setup(r => r.GetNodosAsync()).Returns(new List<Nodo>
            {
                new Nodo { IdNodo = 1, ArrTropas = new Tropa[] { } }
            });

            var service = new NodoService(mockRepo.Object);
            var result = await service.GetNodosAsync();

            Assert.Single(result);
            Assert.Equal(1, result.First().IdNodo);
        }

        [Fact]
        public async Task AddNodoAsync_ThrowsValidationException_WhenDtoIsNull()
        {
            var mockRepo = new Mock<INodoRepository>();
            var service = new NodoService(mockRepo.Object);

            await Assert.ThrowsAsync<ValidationException>(() => service.AddNodoAsync(null!));
        }

        [Fact]
        public async Task DeleteNodoAsync_ThrowsValidationException_WhenIdIsInvalid()
        {
            var mockRepo = new Mock<INodoRepository>();
            var service = new NodoService(mockRepo.Object);

            await Assert.ThrowsAsync<ValidationException>(() => service.DeleteNodoAsync(0));
        }
    }
}
