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
            var newUsuario = new Usuario
            {
                Nombre = userDtoIn.UserName,
                Correo = userDtoIn.Email,
                Password = userDtoIn.Password
            };
            lstUsuarios.Add(newUsuario);

            var userOut = new UserDTOOut
            {
                UserId = id,
                UserName = newUsuario.Nombre,
                Email = newUsuario.Correo,
                Role = BTB.Entities.Enums.Roles.Usuario
            };
            id++;
            return userOut;
        }

        public Task<bool> DeleteUsuarioAsync(int id)
        {
            throw new NotImplementedException();
        }

        public UserDTOOut GetUserFromCredentials(LoginDtoIn loginDtoIn)
        {
            var user = lstUsuarios.FirstOrDefault(u => u.Correo == loginDtoIn.Email && u.Password == loginDtoIn.Password);
            if (user == null) throw new Exception("Credenciales inválidas");

            // In a real implementation, map to UserDTOOut with real id/role
            var userOut = new UserDTOOut
            {
                UserId = 0,
                UserName = user.Nombre,
                Email = user.Correo,
                Role = BTB.Entities.Enums.Roles.Usuario
            };
            return userOut;
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