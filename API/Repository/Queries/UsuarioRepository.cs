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
        public UsuarioRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? "";
        }

        public UserDTOOut AddUserFromCredentials(UserDTOIn userDtoIn)
        {
            var newUsuario = new Usuario
            {
                UsuarioId = id,
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
            var existing = lstUsuarios.FirstOrDefault(u => u.UsuarioId == id);
            if (existing == null) return Task.FromResult(false);
            lstUsuarios.Remove(existing);
            return Task.FromResult(true);
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
            var usuario = lstUsuarios.FirstOrDefault(u => u.UsuarioId == id);
            return Task.FromResult(usuario!);
        }

        public Task<List<Usuario>> GetUsuariosAsync()
        {
            return Task.FromResult(lstUsuarios.ToList());
        }

        public Task<bool> PostUsuarioAsync(Usuario usuario)
        {
            usuario.UsuarioId = id++;
            lstUsuarios.Add(usuario);
            return Task.FromResult(true);
        }

        public Task<bool> PutUsuarioAsync(Usuario usuario)
        {
            var existing = lstUsuarios.FirstOrDefault(u => u.UsuarioId == usuario.UsuarioId);
            if (existing == null) return Task.FromResult(false);
            existing.Nombre = usuario.Nombre;
            existing.Correo = usuario.Correo;
            existing.Password = usuario.Password;
            existing.Visible = usuario.Visible;
            existing.TierId = usuario.TierId;
            existing.Partidas = usuario.Partidas;
            return Task.FromResult(true);
        }
    }
}