using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service;
using BTB.Service.Common;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace BTB.Tests.Service
{
    public class UsuarioServiceTests
    {
        [Fact]
        public void GetUsuariosAsync_ReturnsListOfUsuarios()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["CloudinarySettings:CloudName"]).Returns("test");
            mockConfig.Setup(c => c["CloudinarySettings:ApiKey"]).Returns("test");
            mockConfig.Setup(c => c["CloudinarySettings:ApiSecret"]).Returns("test");
            
            mockRepo.Setup(r => r.GetUsuariosAsync()).Returns(new List<Usuario>
            {
                new Usuario { UsuarioId = 1, Nombre = "User1" }
            });

            var service = new UsuarioService(mockRepo.Object, mockConfig.Object);
            var result = service.GetUsuariosAsync();

            Assert.Single(result);
            Assert.Equal("User1", result.First().Nombre);
        }

        [Fact]
        public async Task AddUsuario_ThrowsArgumentNullException_WhenUsuarioIsNull()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["CloudinarySettings:CloudName"]).Returns("test");
            mockConfig.Setup(c => c["CloudinarySettings:ApiKey"]).Returns("test");
            mockConfig.Setup(c => c["CloudinarySettings:ApiSecret"]).Returns("test");
            
            var service = new UsuarioService(mockRepo.Object, mockConfig.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.AddUsuario(null!));
        }

        [Fact]
        public async Task DeleteUsuario_ReturnsFalse_WhenIdIsInvalid()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["CloudinarySettings:CloudName"]).Returns("test");
            mockConfig.Setup(c => c["CloudinarySettings:ApiKey"]).Returns("test");
            mockConfig.Setup(c => c["CloudinarySettings:ApiSecret"]).Returns("test");
            
            var service = new UsuarioService(mockRepo.Object, mockConfig.Object);

            var result = await service.DeleteUsuario(0);

            Assert.False(result);
        }

        [Fact]
        public async Task AddUserFromCredentials_ThrowsBusinessException_WhenEmailExists()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["CloudinarySettings:CloudName"]).Returns("test");
            mockConfig.Setup(c => c["CloudinarySettings:ApiKey"]).Returns("test");
            mockConfig.Setup(c => c["CloudinarySettings:ApiSecret"]).Returns("test");
            
            mockRepo.Setup(r => r.GetUsuariosAsync()).Returns(new List<Usuario>
            {
                new Usuario { Correo = "test@test.com" }
            });

            var service = new UsuarioService(mockRepo.Object, mockConfig.Object);
            var dto = new UserDTOIn { Email = "test@test.com" };

            await Assert.ThrowsAsync<BusinessException>(() => service.AddUserFromCredentials(dto));
        }
    }
}
