using System.Security.Claims;
using BTB.Entities.DTO;

namespace BTB.Service
{
    public interface IAuthService
    {
        public Task<string> Login(LoginDtoIn userDtoIn);
        public Task<string> Register(UserDTOIn userDtoIn);
        public string GenerateToken(UserDTOOut userDTOOut);
        public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);


    }
}
