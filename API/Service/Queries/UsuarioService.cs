using BTB.Entities.Models;
using BTB.Entities.DTO;
using BTB.Repository.Interfaces;
using BTB.Service.Common;
using System.Threading.Tasks;
using CloudinaryDotNet;
using BTB.Configurations;
using BTB.Utilies;
using CloudinaryDotNet.Actions;

namespace BTB.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly Cloudinary _cloudinary;

        public UsuarioService(IUsuarioRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            CloudinarySettings cloudinarySettings = new CloudinarySettings
            {
                CloudName = configuration["CloudinarySettings:CloudName"],
                ApiKey = configuration["CloudinarySettings:ApiKey"],
                ApiSecret = configuration["CloudinarySettings:ApiSecret"],
            };

            Account account = new Account(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UpdateFotousuario(Usuario usuario, IFormFile foto)
        {
            var imageValidator = new FileValidationHelper(
               new[] { "image/jpeg", "image/png", "image/gif" },
               new[] { ".jpg", ".jpeg", ".png", ".gif" }
           );
           imageValidator.Validate(foto);
           using var stream = foto.OpenReadStream();
            var uploadParams = new ImageUploadParams
           {
               File = new FileDescription(foto.FileName, stream),
               /// Para que le haga un corte bien guapo al tamaño de la foto
               Transformation = new Transformation().Width(400).Height(400).Crop("fill")
               //Transformation = new Transformation().Width(300).Crop("scale").Chain().Effect("cartoonify")
               //Effect(vignette",20), Effect("remove_background", 0.5), Effect("sharpen", 50)  
           };
           var uploadResult = await _cloudinary.UploadAsync(uploadParams);
           usuario.imageUrl = uploadResult.SecureUrl?.ToString();
            _repository.PutUsuarioAsync(usuario);
            return usuario.imageUrl;
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
