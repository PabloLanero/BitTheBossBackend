using BTB.Entities.Models;
using BTB.Entities.DTO;

namespace BTB.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<List<Usuario>> GetUsuariosAsync();
        public Task<Usuario> GetUsuarioByIdAsync(int id);
        public Task<bool> PostUsuarioAsync(Usuario usuario);
        public Task<bool> PutUsuarioAsync(Usuario usuario);
        public Task<bool> DeleteUsuarioAsync(int id);
        public UserDTOOut AddUserFromCredentials(UserDTOIn userDtoIn);
        public UserDTOOut GetUserFromCredentials(LoginDtoIn loginDtoIn);
    }
}