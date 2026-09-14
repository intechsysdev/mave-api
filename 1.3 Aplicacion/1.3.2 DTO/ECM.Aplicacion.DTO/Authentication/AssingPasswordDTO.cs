using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace ECM.Aplicacion.DTO.Authentication
{
    public class AssingPasswordDTO
    {
        [Required]
        public string UserCust { get; set; }

        [Required]
        public string UserSuccli { get; set; }

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
