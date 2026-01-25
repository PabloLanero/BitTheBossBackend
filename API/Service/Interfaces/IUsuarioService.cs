using BTB.Entities.Models;

namespace BTB.Service
{
    public interface IUsuarioService
    {
        public Task<List<Usuario>> GetUsuariosAsync();
        public Task<Usuario> GetUsuarioById(int p_id);
        public Task<bool> AddUsuario(Usuario p_usuario);
        public Task<bool> UpdateUsuario(Usuario p_usuario);
        public Task<bool> DeleteUsuario(int p_id);
    }
}