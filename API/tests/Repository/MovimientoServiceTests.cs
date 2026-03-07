using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service;
using BTB.Service.Common;
using Moq;
using Xunit;

namespace BTB.Tests.Service
{
    public class MovimientoServiceTests
    {
        [Fact]
        public async Task GetMovimientosAsync_ReturnsListOfDTOs()
        {
            var mockRepo = new Mock<IMovimientoRepository>();
            mockRepo.Setup(r => r.GetMovimientosAsync()).Returns(new List<Movimiento>
            {
                new Movimiento { Id = 1, Tropa = new Tropa { Id = 1, Nombre = "T1", Vida = 100, Damage = 10 }, NodoDestino = new Nodo { IdNodo = 1 } }
            });

            var service = new MovimientoService(mockRepo.Object);
            var result = await service.GetMovimientosAsync();

            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }

        [Fact]
        public async Task AddMovimientoAsync_ThrowsValidationException_WhenDtoIsNull()
        {
            var mockRepo = new Mock<IMovimientoRepository>();
            var service = new MovimientoService(mockRepo.Object);

            await Assert.ThrowsAsync<ValidationException>(() => service.AddMovimientoAsync(null!));
        }

        [Fact]
        public async Task DeleteMovimientoAsync_ThrowsValidationException_WhenIdIsInvalid()
        {
            var mockRepo = new Mock<IMovimientoRepository>();
            var service = new MovimientoService(mockRepo.Object);

            await Assert.ThrowsAsync<ValidationException>(() => service.DeleteMovimientoAsync(0));
        }
    }
}
