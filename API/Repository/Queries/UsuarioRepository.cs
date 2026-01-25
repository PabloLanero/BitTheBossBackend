using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;

namespace BTB.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;
        /// <summary>
        /// ASEGURATE DE ELIMINAR ESTA VARIABLE
        /// </summary>
        private List<Usuario> lstUsuarios = new List<Usuario>();
        private int id = 1;
        public UsuarioRepository(IConfiguration p_configuration)
        {
            _connectionString = p_configuration.GetConnectionString("MiSuperConectionString");
        }

        public UserDTOOut AddUserFromCredentials(UserDTOIn userDtoIn)
        {
            Usuario newUsuario = new Usuario
            {
                Nombre= userDtoIn.UserName,
                Correo = userDtoIn.Email,
                Password = userDtoIn.Password
            };
            lstUsuarios.Add(newUsuario);

            throw new NotImplementedException();
        }

        public Task<bool> DeleteUsuarioAsync(int id)
        {
            throw new NotImplementedException();
        }

        public UserDTOOut GetUserFromCredentials(LoginDtoIn loginDtoIn)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Usuario>> GetUsuariosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostUsuarioAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutUsuarioAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}