using System.ComponentModel.DataAnnotations;

namespace ECM.Aplicacion.DTO.Authentication
{
    public class LoginDTO
    {
        [Required]
        public string UserCust { get; set; }

        [Required]
        public string UserSuccli { get; set; }

        public string Password { get; set; }

        public string TokenId { get; set; }

        public string Issuer { get; set; }

        public string TokenRefresh { get; set; }

        public string NewPassword { get; set; }
    }
}
