using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wallpapers.Data;
using Wallpapers.Models;

namespace Wallpapers.ViewModels
{
    public class UploadViewModel
    {
        [RequiredAndAcceptedImageFormat]
        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Debes ingresar un tag")]
        public string TagName { get; set; }
    }

    public class RequiredAndAcceptedImageFormat : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                ErrorMessage = "Este campo es obligatorio";

                return false;
            }

            if (file.ContentType != "image/jpeg"
                && file.ContentType != "image/png")
            {
                ErrorMessage = "El archivo tiene que ser una imagen en formato JPG o PNG";

                return false;
            }

            return true;
        }
    }
}
