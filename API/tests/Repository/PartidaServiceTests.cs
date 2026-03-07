using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service;
using BTB.Service.Common;
using Moq;
using Xunit;

namespace BTB.Tests.Service
{
    public class PartidaServiceTests
    {
        [Fact]
        public async Task GetPartidasAsync_ReturnsListOfDTOs()
        {
            var mockRepo = new Mock<IPartidaRepository>();
            var mockNodoService = new Mock<INodoService>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            
            mockRepo.Setup(r => r.GetPartidasAsync()).Returns(new List<Partida>
            {
                new Partida { IdPartida = "P1", ArrUsuario = new List<Usuario>(), LstNodos = new List<Nodo>() }
            });

            var service = new PartidaService(mockRepo.Object, mockNodoService.Object, mockUsuarioService.Object);
            var result = await service.GetPartidasAsync();

            Assert.Single(result);
            Assert.Equal("P1", result.First().IdPartida);
        }

        [Fact]
        public async Task AddPartidaAsync_ThrowsValidationException_WhenDtoIsNull()
        {
            var mockRepo = new Mock<IPartidaRepository>();
            var mockNodoService = new Mock<INodoService>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var service = new PartidaService(mockRepo.Object, mockNodoService.Object, mockUsuarioService.Object);

            await Assert.ThrowsAsync<ValidationException>(() => service.AddPartidaAsync(null!));
        }

        [Fact]
        public async Task DeletePartidaAsync_ReturnsFalse_WhenIdIsEmpty()
        {
            var mockRepo = new Mock<IPartidaRepository>();
            var mockNodoService = new Mock<INodoService>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var service = new PartidaService(mockRepo.Object, mockNodoService.Object, mockUsuarioService.Object);

            var result = await service.DeletePartidaAsync("");

            Assert.False(result);
        }
    }
}
