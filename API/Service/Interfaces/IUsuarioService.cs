using BTB.Entities.Models;
using BTB.Entities.DTO;

namespace BTB.Service
{
    public interface IUsuarioService
    {
        public List<Usuario> GetUsuariosAsync();
        public Task<Usuario> GetUsuarioById(int p_id);
        public Task<Usuario> AddUsuario(Usuario p_usuario);
        public Task<Usuario> UpdateUsuario(Usuario p_usuario);
        public Task<bool> DeleteUsuario(int p_id);

        // Cloudinary
        public Task<string> UpdateFotousuario(Usuario usuario, IFormFile foto);

        // Credential-based helpers
        public Task<UserDTOOut> AddUserFromCredentials(UserDTOIn userDtoIn);
        public Task<UserDTOOut> GetUserFromCredentials(LoginDtoIn loginDtoIn);
    }
}