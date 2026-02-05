using BTB.Entities.Models;
using BTB.Entities.DTO;
using BTB.Repository.Interfaces;

namespace BTB.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> AddUsuario(Usuario p_usuario)
        {
            if (p_usuario == null) throw new ArgumentNullException(nameof(p_usuario));
            return _repository.PostUsuarioAsync(p_usuario);
        }

        public Task<bool> DeleteUsuario(int p_id)
        {
            if (p_id <= 0) return Task.FromResult(false);
            return _repository.DeleteUsuarioAsync(p_id);
        }

        public Task<Usuario> GetUsuarioById(int p_id)
        {
            return _repository.GetUsuarioByIdAsync(p_id);
        }

        public Task<List<Usuario>> GetUsuariosAsync()
        {
            return _repository.GetUsuariosAsync();
        }

        public Task<bool> UpdateUsuario(Usuario p_usuario)
        {
            if (p_usuario == null) throw new ArgumentNullException(nameof(p_usuario));
            return _repository.PutUsuarioAsync(p_usuario);
        }

        public UserDTOOut AddUserFromCredentials(UserDTOIn userDtoIn)
        {
            if (userDtoIn == null) throw new ArgumentNullException(nameof(userDtoIn));
            return _repository.AddUserFromCredentials(userDtoIn);
        }

        public UserDTOOut GetUserFromCredentials(LoginDtoIn loginDtoIn)
        {
            if (loginDtoIn == null) throw new ArgumentNullException(nameof(loginDtoIn));
            return _repository.GetUserFromCredentials(loginDtoIn);
        }
    }
}
