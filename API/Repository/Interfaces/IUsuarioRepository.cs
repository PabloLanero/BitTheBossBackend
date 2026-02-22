using BTB.Entities.Models;
using BTB.Entities.DTO;

namespace BTB.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        public List<Usuario> GetUsuariosAsync();
        public Task<Usuario> GetUsuarioByIdAsync(int id);
        public Task<Usuario> PostUsuarioAsync(Usuario usuario);
        public Task<Usuario> PutUsuarioAsync(Usuario usuario);
        public Task<Usuario> DeleteUsuarioAsync(int id);
        public Task<UserDTOOut> AddUserFromCredentials(UserDTOIn userDtoIn);
        public Task<UserDTOOut> GetUserFromCredentials(LoginDtoIn loginDtoIn);
    }
}