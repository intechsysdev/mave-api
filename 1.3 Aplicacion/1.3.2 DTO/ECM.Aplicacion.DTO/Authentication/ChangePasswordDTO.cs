using System.ComponentModel.DataAnnotations;

namespace ECM.Aplicacion.DTO.Authentication
{
    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
