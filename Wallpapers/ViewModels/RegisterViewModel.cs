using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wallpapers.Data;
using Wallpapers.Models;

namespace Wallpapers.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nombre de usuario")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas ingresadas no coinciden")]
        public string PasswordConfirmation { get; set; }
    }
}
