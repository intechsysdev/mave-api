namespace ECM.Aplicacion.DTO.Authentication
{
    public class LoginResultDTO
    {
        public UserLoginDTO User { get; set; }

        public CompanyDTO Company { get; set; }

        public bool Valid { get; set; }

        public string TokenSession { get; set; }

        public string Jwt { get; set; }
                
        public bool RequiredPasswordChange { get; set; }

        public bool TwoFactorAuth { get; set; }
    }
}
