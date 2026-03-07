using BTB.Entities.DTO;
using BTB.Service;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Security.Claims;
using Xunit;

namespace BTB.Tests.Service
{
    public class AuthServiceTests
    {
        [Fact]
        public void GenerateToken_ReturnsValidToken()
        {
            var mockConfig = new Mock<IConfiguration>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            
            mockConfig.Setup(c => c["JWT:SecretKey"]).Returns("SuperSecretKeyForTestingPurposesOnly123456");
            mockConfig.Setup(c => c["JWT:ValidIssuer"]).Returns("TestIssuer");
            mockConfig.Setup(c => c["JWT:ValidAudience"]).Returns("TestAudience");

            var service = new AuthService(mockConfig.Object, mockUsuarioService.Object);
            var userDto = new UserDTOOut { UserId = 1, UserName = "Test", Email = "test@test.com", Role = "Usuario" };

            var token = service.GenerateToken(userDto);

            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void HasAccessToResource_ReturnsTrue_WhenUserIsOwner()
        {
            var mockConfig = new Mock<IConfiguration>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var service = new AuthService(mockConfig.Object, mockUsuarioService.Object);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Usuario")
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var result = service.HasAccessToResource(1, principal);

            Assert.True(result);
        }

        [Fact]
        public void HasAccessToResource_ReturnsFalse_WhenUserIsNotOwner()
        {
            var mockConfig = new Mock<IConfiguration>();
            var mockUsuarioService = new Mock<IUsuarioService>();
            var service = new AuthService(mockConfig.Object, mockUsuarioService.Object);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Usuario")
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var result = service.HasAccessToResource(2, principal);

            Assert.False(result);
        }
    }
}
