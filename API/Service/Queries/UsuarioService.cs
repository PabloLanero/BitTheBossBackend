using BTB.Entities.Models;
using BTB.Entities.DTO;
using BTB.Repository.Interfaces;
using BTB.Service.Common;
using System.Threading.Tasks;

namespace BTB.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Usuario> AddUsuario(Usuario p_usuario)
        {
            if (p_usuario == null) throw new ArgumentNullException(nameof(p_usuario));
            return await _repository.PostUsuarioAsync(p_usuario);
        }

        public async Task<bool> DeleteUsuario(int p_id)
        {
            if (p_id <= 0) return false;
            return await _repository.DeleteUsuarioAsync(p_id);
        }

        public async Task<Usuario> GetUsuarioById(int p_id)
        {
            return await _repository.GetUsuarioByIdAsync(p_id);
        }

        public List<Usuario> GetUsuariosAsync()
        {
            return _repository.GetUsuariosAsync();
        }

        public Task<Usuario> UpdateUsuario(Usuario p_usuario)
        {
            if (p_usuario == null) throw new ArgumentNullException(nameof(p_usuario));
            return _repository.PutUsuarioAsync(p_usuario);
        }

        public async Task<UserDTOOut> AddUserFromCredentials(UserDTOIn userDtoIn)
        {
            if (userDtoIn == null) throw new ValidationException("Datos de usuario inválidos");

            // simple business validation: email uniqueness
            var all =  _repository.GetUsuariosAsync();
            if (all.Any(u => string.Equals(u.Correo, userDtoIn.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new BusinessException("Ya existe un usuario con ese correo");
            }
            
            return await _repository.AddUserFromCredentials(userDtoIn);
        }

        public async Task<UserDTOOut> GetUserFromCredentials(LoginDtoIn loginDtoIn)
        {
            if (loginDtoIn == null) throw new ValidationException("Credenciales inválidas");
            try
            {
                return await _repository.GetUserFromCredentials(loginDtoIn);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
