using BTB.Entities.Models;
using BTB.Entities.DTO;

namespace BTB.Service
{
    public interface IUsuarioService
    {
        public Task<List<Usuario>> GetUsuariosAsync();
        public Task<Usuario> GetUsuarioById(int p_id);
        public Task<bool> AddUsuario(Usuario p_usuario);
        public Task<bool> UpdateUsuario(Usuario p_usuario);
        public Task<bool> DeleteUsuario(int p_id);

        // Credential-based helpers
        public UserDTOOut AddUserFromCredentials(UserDTOIn userDtoIn);
        public UserDTOOut GetUserFromCredentials(LoginDtoIn loginDtoIn);
    }
}