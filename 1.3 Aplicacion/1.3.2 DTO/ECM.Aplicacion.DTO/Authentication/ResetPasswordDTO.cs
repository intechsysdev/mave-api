using System.ComponentModel.DataAnnotations;

namespace ECM.Aplicacion.DTO.Authentication
{
    public class ResetPasswordDTO
    {
        [Required]
        public string UserCust { get; set; }

        [Required]
        public string UserSuccli { get; set; }

        [Required]
        public string Email { get; set; }

    }
}
