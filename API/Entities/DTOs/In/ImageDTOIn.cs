using System.ComponentModel.DataAnnotations;

namespace BTB.Entities.DTO
{
    public class UploadImageDTO{
        [Required(ErrorMessage = "Debe proporcionar una imagen.")]
        public IFormFile Imagen { get; set; }
    }
}
