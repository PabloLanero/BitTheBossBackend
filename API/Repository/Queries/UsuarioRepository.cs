using BTB.Data;
using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BTB.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BTBContext _context;

        public UsuarioRepository(BTBContext context)
        {
            _context = context;
        }

        public async Task<UserDTOOut> AddUserFromCredentials(UserDTOIn userDtoIn)
        {
            var newUsuario = new Usuario
            {
                Nombre = userDtoIn.UserName,
                Correo = userDtoIn.Email,
                Password = userDtoIn.Password,
                Rol = BTB.Entities.Enums.Roles.Usuario,
            };

            Usuario user = _context.Usuarios.Add(newUsuario).Entity;
            await _context.SaveChangesAsync();
            await _context.Entry(user).Reference(u => u.Tier).LoadAsync();

            var userOut = new UserDTOOut
            {
                UserId = user.UsuarioId,
                UserName = user.Nombre,
                Email = user.Correo,
                Role = user.Rol,
                Tier = user.Tier?.Titulo ?? "Bronce"
            };

            return userOut;
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            Usuario usuario = _context.Remove<Usuario>(new Usuario { UsuarioId = id }).Entity;
            return usuario != null;
        }

        public async Task<UserDTOOut> GetUserFromCredentials(LoginDtoIn loginDtoIn)
        {
            Usuario? usuario = await _context.Usuarios
                .Include(u => u.Tier)
                .FirstOrDefaultAsync(u =>
                    u.Correo == loginDtoIn.Email &&
                    u.Password == loginDtoIn.Password &&
                    u.Visible);

            if (usuario == null) throw new Exception("Credenciales invalidas");

            var userOut = new UserDTOOut
            {
                UserId = usuario.UsuarioId,
                UserName = usuario.Nombre,
                Email = usuario.Correo,
                Role = usuario.Rol,
                Tier = usuario.Tier?.Titulo ?? "Bronce"
            };

            return userOut;
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            Usuario? usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) throw new Exception("Credenciales invalidas");

            return usuario;
        }

        public List<Usuario> GetUsuariosAsync()
        {
            IEnumerable<Usuario> usuarios = _context.Usuarios.AsQueryable().Where(p => p.Visible);
            return usuarios.ToList();
        }

        public async Task<Usuario> PostUsuarioAsync(Usuario newUsuario)
        {
            Usuario usuario = _context.Usuarios.Add(newUsuario).Entity;
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> PutUsuarioAsync(Usuario updateUsuario)
        {
            Usuario usuario = _context.Usuarios.Update(updateUsuario).Entity;
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
